using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Sandbox;
using grocery_mate_backend.Services;
using grocery_mate_backend.Services.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        var identityUser = new IdentityUser() { UserName = userDo.EmailAddress, Email = userDo.EmailAddress };

        var result = await _userManager.CreateAsync(
            identityUser,
            userDto.Password
        );

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                GmLogger.GetInstance()?.Trace(methodName, error.Description);
            }

            GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State");
            return BadRequest("Sorry, no new login could be created please check your details and try again. ");
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
        const string methodName = "REST Log-In";

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

    [Authorize]
    [HttpGet("settings")]
    public async Task<ActionResult<UserDataDto>> GetUserSettings([FromQuery] UserDataRequestDto userMail)
    {
        const string methodName = "REST Get User-Settings";

        if (!ModelState.IsValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State due to Bad credentials");
            return BadRequest("Bad credentials");
        }

        var user = await FindUser(userMail.email);
        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User does not exist");
            return BadRequest("Bad credentials");
        }

        var address = await _context.Address
            .Where(a => a.AddressId == user.AddressId)
            .FirstOrDefaultAsync() ?? new Address();

        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost("settings")]
    public async Task<ActionResult> UpdateUserSettings(UpdateUserSettingsDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (!ModelState.IsValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid Model-State due to Bad credentials");
            return BadRequest("Bad credentials");
        }

        var user = await FindUser(requestDto.email);
        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User does not exist");
            return BadRequest("Bad credentials");
        }

        var address = await FindAddress(requestDto.Address);
        if (user.AddressId == null)
        {
            if (address == null)
            {
                address = new Address(requestDto.Address);
                _context.Add(address);
            }
        }
        else
        {
            var oldAddress = await _context.Address
                .Where(a => a.AddressId == user.AddressId)
                .FirstOrDefaultAsync();

            if (oldAddress != null)
            {
                oldAddress.Users.Remove(user);
                if (oldAddress.Users.IsNullOrEmpty())
                    _context.Remove(oldAddress);
            }

            if (address == null)
            {
                address = new Address(requestDto.Address);
                _context.Add(address);
            }
        }
        address.Users.Add(user);
        user.AddressId = address.AddressId;
        
        user.FirstName = requestDto.User.FirstName;
        user.SecondName = requestDto.User.SecondName;
        user.EmailAddress = requestDto.User.EmailAddress;
        user.ResidencyDetails = requestDto.User.ResidencyDetails;
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<User?> FindUser(string email)
    {
        return await _context.User
            .Where(u => u.EmailAddress == email)
            .FirstOrDefaultAsync();
    }

    private async Task<Address?> FindAddress(AddressDto address)
    {
        var user = await _context.Address
            .Where(a =>
                a.Street == address.Street &&
                a.HouseNr == address.HouseNr.ToString() &&
                a.City == address.City &&
                a.ZipCode == address.ZipCode)
            .FirstOrDefaultAsync();
        return user;
    }
}