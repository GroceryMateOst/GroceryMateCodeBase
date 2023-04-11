using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/settings")]
public class UserSettingsController : BaseController
{
    public UserSettingsController(GroceryContext context, UserManager<IdentityUser> userManager,
        JwtService jwtService) : base(context, userManager, jwtService)
    {
    }


    [Authorize]
    [HttpGet("settings")]
    public async Task<ActionResult<UserDataDto>> GetUserSettings([FromQuery] UserDataRequestDto userMail)
    {
        const string methodName = "REST Get User-Settings";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");
        
        var user = _userService.FindUser(userMail.email).Result;
        if (!ValidationBase.ValidateUser(user, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        var address = _addressService.FindAddressByGuid(user.AddressId).Result;
        if (!ValidationBase.ValidateAddress(address, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost("settings")]
    public async Task<ActionResult> UpdateUserSettings(UpdateUserSettingsDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var user = await _userService.FindUser(requestDto.email);
        if (!ValidationBase.ValidateUser(user, methodName)) 
            return BadRequest("User with given eMail-Adr. not found");

        var newAddress = await _addressService.FindOrCreateAddress(requestDto.Address);
        var oldAddress = _addressService.FindAddressByGuid(user.AddressId).Result;

        if (!ValidationBase.ValidateAddress(oldAddress, methodName))
            _addressService.RemoveAddress(oldAddress, user);

        newAddress.Users.Add(user);
        user.AddressId = newAddress.AddressId;
        user.ResidencyDetails = requestDto.User.ResidencyDetails;

        await _context.SaveChangesAsync();
        return Ok();
    }
}