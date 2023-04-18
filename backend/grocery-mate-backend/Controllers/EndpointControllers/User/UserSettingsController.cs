using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/User/Settings")]
public class UserSettingsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public UserSettingsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDataDto>> GetUserSettings([FromQuery] UserDataRequestDto userMail)
    {
        const string methodName = "REST Get User-Settings";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var user = _unitOfWork.User.FindUserByMail(userMail.email).Result;
        if (!UserValidation.ValidateUser(user, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        var address = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result;
        if (!ValidationBase.ValidateAddress(address, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateUserSettings(UpdateUserSettingsDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var user = _unitOfWork.User.FindUserByMail(requestDto.email);
        if (!UserValidation.ValidateUser(user, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        var newAddress = _unitOfWork.Address.FindOrCreateUserAddress(requestDto.Address);
        var oldAddress = _unitOfWork.Address.FindAddressByGuid(user.Result.AddressId).Result;

        if (!ValidationBase.ValidateAddress(oldAddress, methodName))
            await _unitOfWork.Address.RemoveAddress(oldAddress, await user);

        await _unitOfWork.Authentication.Add(user.Result);
        user.Result.AddressId = newAddress.Result?.AddressId;
        user.Result.ResidencyDetails = requestDto.User.ResidencyDetails;

        await _unitOfWork.CompleteAsync();
        return Ok();
    }
}