using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/User/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
    {
        var userDm = new User(userDto);

        if (!ValidationBase.ValidateModel(ModelState))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var identityUser = new IdentityUser() {UserName = userDm.EmailAddress, Email = userDm.EmailAddress};
        var result = _unitOfWork.Authentication.SaveNewIdentityUser(identityUser, userDm).Result;

        if (!AuthenticationValidation.ValidateIdentityUserCreation(result, LogMessages.MethodName_REST_POST_register))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        userDm.Identity = identityUser;
        await _unitOfWork.Authentication.Add(userDm);
        await _unitOfWork.CompleteAsync();

        GmLogger.Instance.Trace(LogMessages.MethodName_REST_POST_register, LogMessages.LogMessage_SuccessfullyCreated);

        userDto.Password = string.Empty;
        Created(string.Empty, userDto);
        
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        if (!ValidationBase.ValidateModel(ModelState))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var identityUser = _unitOfWork.Authentication.FindIdentityUser(requestDto.EmailAddress).Result;
        if (!UserValidation.ValidateUser(identityUser))
            return BadRequest(ResponseErrorMessages.InvalidLogin);

        var passwordCheck = _unitOfWork.Authentication.CheckPassword(identityUser, requestDto.Password);
        if (!AuthenticationValidation.ValidateUserPassword(passwordCheck.Result))
            return BadRequest(ResponseErrorMessages.InvalidLogin);

        var user = await _unitOfWork.User.FindUserByIdentityId(identityUser.Id);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        var token = _unitOfWork.Authentication.CreateToken(identityUser, user.UserId);
        GmLogger.Instance.Trace(LogMessages.MethodName_REST_POST_login, LogMessages.LogMessage_BearerTokenGenerated);
        return Ok(token);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<AuthenticationResponseDto>> CancelBearerToken()
    {
        const string oldValue = "Bearer ";
        var newValue = string.Empty;
        
        var token = Request.Headers["Authorization"].ToString().Replace(oldValue, newValue);
        await _unitOfWork.TokenBlacklist.AddTokenToBlacklist(token);

        GmLogger.Instance.Trace(LogMessages.MethodName_REST_POST_logout, LogMessages.LogMessage_TokenRevoked);
        return Ok();
    }
}