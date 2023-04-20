using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Utility.Log;
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
        if (!ModelState.IsValid)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid request model!");
            return BadRequest("Invalid Request!");
        }

        var user = _unitOfWork.User.FindUserByMail(userMail.email).Result;
        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given eMail-Adr. not found");
            return BadRequest("User with given eMail-Adr. not found");
        }

        var address = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result ?? new Address();
        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateUserSettings(UpdateUserSettingsDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var user = await _unitOfWork.User.FindUserByMail(requestDto.email);
        if (!UserValidation.ValidateUser(user, methodName))
            return BadRequest("User with given eMail-Adr. not found");

        var newAddress = await _unitOfWork.Address.FindOrCreateUserAddress(requestDto.Address);
        var oldAddress = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result;
        if (!ValidationBase.ValidateAddress(oldAddress, methodName))
            await _unitOfWork.Address.RemoveAddress(oldAddress, user);

        user.AddressId = newAddress?.AddressId;
        user.ResidencyDetails = requestDto.User.ResidencyDetails;

        await _unitOfWork.CompleteAsync();
        return Ok();
    }
}