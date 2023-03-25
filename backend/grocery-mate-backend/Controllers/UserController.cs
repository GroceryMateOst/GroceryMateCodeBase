using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using grocery_mate_backend.Sandbox;
using Microsoft.EntityFrameworkCore;
using grocery_mate_backend.Services;
using grocery_mate_backend.Services.Utility;
using NLog;
using User = grocery_mate_backend.Models.User;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class UserController : ControllerBase
{
    private readonly GroceryContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtService _jwtService;

    public UserController(GroceryContext context, UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _context = context;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(CreateUserUserDto userDto)
    {
        const string methodName = "REST Create User";

        var userDo = new User(userDto);

        GmLogger.GetInstance()?.Trace(methodName, "Validation-State: " + ModelState.IsValid);

        if (!ModelState.IsValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State");
            return BadRequest("The specified user data does not correspond to the specifications");
        }

        var identityUser = new IdentityUser() {UserName = userDo.EmailAddress, Email = userDo.EmailAddress};

        var result = await _userManager.CreateAsync(
            identityUser,
            userDto.Password
        );

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                GmLogger.GetInstance()?.Warn(methodName, error.Description);
                return BadRequest(result.Errors);
            }

            return BadRequest(result.Errors);
        }

        userDo.Identity = identityUser;
        _context.Add(userDo);
        await _context.SaveChangesAsync();
        
        GmLogger.GetInstance()?.Trace(methodName, "successfully created");

        userDto.Password = Symbols.Empty;
        return Created("", userDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> CreateBearerToken(AuthenticationRequestDto requestDto)
    {
        const string methodName = "REST Create User";

        if (!ModelState.IsValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State due to Bad credentials");
            return BadRequest("Bad credentials");
        }

        var user = await _userManager.FindByNameAsync(requestDto.EmailAddress);

        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

        if (!isPasswordValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid Password");
            return BadRequest("Bad credentials");
        }

        var token = _jwtService.CreateToken(user);

        GmLogger.GetInstance()?.Trace(methodName, "Bearer-Token Successfully generated");
        return Ok(token);
    }
}