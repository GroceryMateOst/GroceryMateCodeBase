using System.Globalization;
using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/[controller]")]
public class ShoppingController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public ShoppingController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("groceryRequest")]
    public async Task<ActionResult<GroceryRequestDto>> PostGroceryRequest(GroceryRequestDto requestDto)
    {
        const string methodName = "POST Grocery-Request";

        if (!ValidationBase.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var identityUserNam = User.Identity?.Name;
        if (identityUserNam == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "Couldn't find user by give JWT-Token");
            return BadRequest("Couldn't authenticate user due bad credentials");
        }

        var id = (await _unitOfWork.Authentication.FindIdentityUser(identityUserNam)).Id;
        if (id == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given identityId not found");
            return BadRequest("User not found");
        }

        var user = await _unitOfWork.User.FindUserByIdentityId(id);

        if (user == null)
        {
            GmLogger.GetInstance()?.Warn(methodName, "User with given identityId does not exist");
            return BadRequest("User not found");
        }

        if (!GroceryValidation.ValidateRequestState(requestDto.RequestState))
        {
            GmLogger.GetInstance()?.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest("Invalid request");
        }

        DateTime fromDate;
        DateTime toDate;
        try
        {
            fromDate = DateTime.Parse(requestDto.FromDate, null).ToUniversalTime();
            toDate = DateTime.Parse(requestDto.ToDate, null).ToUniversalTime();
        }
        catch (Exception e)
        {
            GmLogger.GetInstance()?.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest("Invalid date");
        }
        var groceryRequest = new GroceryRequest(user, requestDto, fromDate, toDate);

        try
        {
            await _unitOfWork.Shopping.Add(groceryRequest);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            GmLogger.GetInstance()?.Trace(methodName, e.Message);
            return BadRequest("Something went wrong! Pls. try again");
        }

        GmLogger.GetInstance()?.Trace(methodName, "Grocery-Request successfully saved");
        return Ok();
    }
}