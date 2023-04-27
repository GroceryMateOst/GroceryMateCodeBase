using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/User/[controller]")]
public class AuthenticationController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
    {
        const string methodName = "REST Create User";
        var userDm = new User(userDto);

        if (!ValidationBase.ValidateModel(ModelState))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var identityUser = new IdentityUser() {UserName = userDm.EmailAddress, Email = userDm.EmailAddress};
        var result = _unitOfWork.Authentication.SaveNewIdentityUser(identityUser, userDm).Result;

        if (!AuthenticationValidation.ValidateIdentityUserCreation(result, methodName))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        userDm.Identity = identityUser;
        await _unitOfWork.Authentication.Add(userDm);
        await _unitOfWork.CompleteAsync();

        GmLogger.Instance.Trace(methodName, "successfully created");

        userDto.Password = string.Empty;
        return Created("", userDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        const string methodName = "REST Log-In";

        if (!ValidationBase.ValidateModel(ModelState))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var identityUser = _unitOfWork.Authentication.FindIdentityUser(requestDto.EmailAddress).Result;
        if (!UserValidation.ValidateUser(identityUser, methodName))
            return BadRequest(ResponseErrorMessages.InvalidLogin);

        var passwordCheck = _unitOfWork.Authentication.CheckPassword(identityUser, requestDto.Password);
        if (!AuthenticationValidation.ValidateUserPassword(passwordCheck.Result, methodName))
            return BadRequest(ResponseErrorMessages.InvalidLogin);

        var token = _unitOfWork.Authentication.CreateToken(identityUser);
        GmLogger.Instance.Trace(methodName, "Bearer-Token Successfully generated");
        return Ok(token);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<AuthenticationResponseDto>> CancelBearerToken()
    {
        const string methodName = "REST Log-Out";
        var token =  Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _unitOfWork.TokenBlacklist.AddTokenToBlacklist(token);
   
        GmLogger.Instance.Trace(methodName, "Token successfully revoked");
        return Ok();
    }
}