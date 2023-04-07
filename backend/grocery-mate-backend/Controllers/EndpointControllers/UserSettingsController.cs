using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/settings")]
public class UserSettingsController : BaseController
{
    public UserSettingsController(GroceryContext context, UserManager<IdentityUser> userManager,
        JwtService jwtService) : base(context, userManager, jwtService) { }

    [Authorize]
    [HttpGet("settings")]
    public async Task<ActionResult<UserDataDto>> GetUserSettings([FromQuery] UserDataRequestDto userMail)
    {
        const string methodName = "REST Get User-Settings";

        if (AuthenticationValidation.ValidateModelState(ModelState, methodName)) return BadRequest("Invalid Model-State due to Bad credentials");
        
        var user = await FindUser(userMail.email);
        if (ValidationBase.ValidateUser(user, methodName)) return BadRequest("User with given eMail-Adr. not found");

        var address = FindAddressByGuid(user.AddressId).Result;
        if (ValidationBase.ValidateAddress(address, methodName)) return BadRequest("User with given eMail-Adr. not found");
        
        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost("settings")]
    public async Task<ActionResult> UpdateUserSettings(UpdateUserSettingsDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (AuthenticationValidation.ValidateModelState(ModelState, methodName)) return BadRequest("Invalid Model-State due to Bad credentials");
        
        var user = await FindUser(requestDto.email);
        if (ValidationBase.ValidateUser(user, methodName)) return BadRequest("User with given eMail-Adr. not found");
        
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

        return Ok("Settings have been updated successfully");
    }
}