using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("api/v0/User/Settings")]
public class UserSettingsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserSettingsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDataDto>> GetUserSettings()
    {
        if (!AuthenticationValidation.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null)
        {
            GmLogger.Instance.Warn(LogMessages.MethodName_REST_GET_settings,
                LogMessages.LogMessage_UserWithIdDoesntExist);
            return BadRequest(ResponseErrorMessages.SettingsError);
        }

        var address = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result ?? new Address();
        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateUserSettings(UserDataDto requestDto)
    {

        if (!AuthenticationValidation.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null)
        {
            GmLogger.Instance.Warn(LogMessages.MethodName_REST_POST_settings, LogMessages.LogMessage_UserWithIdDoesntExist);
            return BadRequest(ResponseErrorMessages.NotAuthorised);
        }

        Address? newAddress;
        try
        {
            newAddress = await _unitOfWork.Address.FindOrCreateUserAddress(requestDto.Address);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.MethodName_REST_POST_settings, e.Message);
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        var oldAddress = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result;
        if (!AddressValidation.ValidateAddress(oldAddress))
            await _unitOfWork.Address.RemoveAddress(oldAddress, user);

        user.AddressId = newAddress?.AddressId;
        user.ResidencyDetails = requestDto.User.ResidencyDetails;

        await _unitOfWork.CompleteAsync();
        return Ok();
    }

    [Authorize]
    [HttpGet("getCity")]
    public async Task<ActionResult<ZipCodeResponseDto>> GetCityNameByZip([FromQuery] int zipCode)
    {
        var zipCodeResponseDto = await GeoApifyApi.GetCityName(zipCode);
        return Ok(zipCodeResponseDto);
    }
}