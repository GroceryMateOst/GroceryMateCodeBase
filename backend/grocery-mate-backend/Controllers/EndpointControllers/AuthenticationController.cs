using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Sandbox;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/authentication")]
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

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var identityUser = new IdentityUser() {UserName = userDm.EmailAddress, Email = userDm.EmailAddress};
        var result = _unitOfWork.Authentication.SaveNewIdentityUser(identityUser, userDm).Result;

        if (!AuthenticationValidation.ValidateIdentityUserCreation(result, methodName))
            return BadRequest("Invalid User-details");

        userDm.Identity = identityUser;
        await _unitOfWork.Authentication.Add(userDm);
        await _unitOfWork.CompleteAsync();

        GmLogger.GetInstance()?.Trace(methodName, "successfully created");

        userDto.Password = Symbols.Empty;
        return Created("", userDto);
    }

    [HttpPost("login")]
    public Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        const string methodName = "REST Log-In";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return Task.FromResult<ActionResult<AuthenticationResponseDto>>(BadRequest("Invalid Request!"));

        var identityUser = _unitOfWork.Authentication.FindIdentityUser(requestDto.EmailAddress).Result;
        if (!UserValidation.ValidateUser(identityUser, methodName))
            return Task.FromResult<ActionResult<AuthenticationResponseDto>>(
                BadRequest("User with given eMail-Adr. not found"));

        var passwordCheck = _unitOfWork.Authentication.CheckPassword(identityUser, requestDto.Password);
        if (!AuthenticationValidation.ValidateUserPassword(passwordCheck.Result, methodName))
            return Task.FromResult<ActionResult<AuthenticationResponseDto>>(BadRequest("Invalid Username or Password"));

        var token = _unitOfWork.Authentication.CreateToken(identityUser);
        GmLogger.GetInstance()?.Trace(methodName, "Bearer-Token Successfully generated");
        return Task.FromResult<ActionResult<AuthenticationResponseDto>>(Ok(token));
    }
}