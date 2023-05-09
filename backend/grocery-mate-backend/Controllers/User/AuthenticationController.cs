using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Data.DataModels.UserManagement;

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

    /**
     * REST-Method:     POST
     * Path:            api/v0/User/Authentication/register
     * Request-DTO:     CreateUserDto
     * Response-DTO:    User
     * token required:  false
     */
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

    /**
     * REST-Method:     POST
     * Path:            api/v0/User/Authentication/login
     * Request-DTO:     AuthenticationRequestDto
     * Response-DTO:    AuthenticationResponseDto
     * token required:  false
     */
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        const string methodName = "REST Log-In";

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
        GmLogger.Instance.Trace(methodName, "Bearer-Token Successfully generated");
        return Ok(token);
    }

    /**
     * REST-Method:     POST
     * Path:            api/v0/User/Authentication/logout
     * Request-DTO:     -
     * Response-DTO:    AuthenticationResponseDto
     * token required:  true
     */
    [HttpPost("logout")]
    public async Task<ActionResult<AuthenticationResponseDto>> CancelBearerToken()
    {
        const string methodName = "REST Log-Out";
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _unitOfWork.TokenBlacklist.AddTokenToBlacklist(token);

        GmLogger.Instance.Trace(methodName, "Token successfully revoked");
        return Ok();
    }
}