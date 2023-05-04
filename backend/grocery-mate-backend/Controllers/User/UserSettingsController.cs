using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers;

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
        public async Task<ActionResult<UserDataDto>> GetUserSettings()
        {
            const string methodName = "REST Get User-Settings";
            
            if (!AuthenticationValidation.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
                return BadRequest(ResponseErrorMessages.InvalidRequest);

            var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
            if (user == null)
            {
                GmLogger.Instance.Warn(methodName, "User with given identityId does not exist");
                return BadRequest(ResponseErrorMessages.SettingsError);
            }

        var address = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result ?? new Address();
        return Ok(new UserDataDto(user, address));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UpdateUserSettings(UserDataDto requestDto)
    {
        const string methodName = "REST Set User-Settings";

        if (!AuthenticationValidation.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
            return BadRequest(ResponseErrorMessages.InvalidRequest);

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null)
        {
            GmLogger.Instance.Warn(methodName, "User with given identityId does not exist");
            return BadRequest(ResponseErrorMessages.NotAuthorised);
        }

        Address? newAddress;
        try
        {
            newAddress = await _unitOfWork.Address.FindOrCreateUserAddress(requestDto.Address);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Warn(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }
        
        var oldAddress = _unitOfWork.Address.FindAddressByGuid(user.AddressId).Result;
        if (!ValidationBase.ValidateAddress(oldAddress, methodName))
            await _unitOfWork.Address.RemoveAddress(oldAddress, user);

        user.AddressId = newAddress?.AddressId;
        user.ResidencyDetails = requestDto.User.ResidencyDetails;

        await _unitOfWork.CompleteAsync();
        return Ok();
    }
}