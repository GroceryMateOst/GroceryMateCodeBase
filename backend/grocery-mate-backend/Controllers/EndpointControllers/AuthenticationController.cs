using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;
using grocery_mate_backend.Services;
using grocery_mate_backend.Services.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/authentication")]
public class AuthenticationController : BaseController
{
    public AuthenticationController(GroceryContext context, UserManager<IdentityUser> userManager,
        JwtService jwtService) : base(context, userManager, jwtService)
    {
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
    {
        const string methodName = "REST Create User";
        var userDm = new User(userDto);

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Bad credentials");

        var identityUser = new IdentityUser() {UserName = userDm.EmailAddress, Email = userDm.EmailAddress};
        var result = _authenticationService.SaveNewIdentityUser(identityUser, userDm).Result;

        if (!AuthenticationValidation.ValidateIdentityUserCreation(result, methodName))
            return BadRequest(result.Errors.ElementAt(0).Description);

        userDm.Identity = identityUser;
        _context.AddRange(userDm);
        await _context.SaveChangesAsync();

        GmLogger.GetInstance()?.Trace(methodName, "successfully created");

        userDto.Password = Symbols.Empty;
        return Created("", userDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        const string methodName = "REST Log-In";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Bad credentials");

        var identityUser = _authenticationService.FindIdentityUser(requestDto.EmailAddress).Result;
        if (!ValidationBase.ValidateUser(identityUser, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        var passwordCheck = _authenticationService.CheckPassword(identityUser, requestDto.Password);
        if (!AuthenticationValidation.ValidateUserPassword(passwordCheck.Result, methodName))
            return BadRequest("Invalid Password");

        var token = _jwtService.CreateToken(identityUser);
        GmLogger.GetInstance()?.Trace(methodName, "Bearer-Token Successfully generated");
        return Ok(token);
    }
}