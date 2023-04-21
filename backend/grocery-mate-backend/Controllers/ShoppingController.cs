using System.Globalization;
using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Service;
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

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null)
        {
            return BadRequest("User is not authenticated");
        }

        if (!GroceryValidation.ValidateRequestState(requestDto.RequestState))
        {
            GmLogger.GetInstance()?.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest("Invalid request");
        }

        try
        {
            var fromDate = DateTime.Parse(requestDto.FromDate, null).ToUniversalTime();
            var toDate = DateTime.Parse(requestDto.ToDate, null).ToUniversalTime();
            var groceryRequest = new GroceryRequest(user, requestDto, fromDate, toDate);

            await _unitOfWork.Shopping.Add(groceryRequest, user);
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

    [HttpGet("groceryRequest")]
    public async Task<ActionResult<GroceryResponseDto>> GetAllGroceryRequest()
    {
        const string methodName = "POST Grocery-Request";

        if (!ValidationBase.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        var groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequests();
        
        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address));
        }

        return Ok(requests);
    }
}