using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using grocery_mate_backend.BusinessLogic.Validation.Shopping;
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

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            !GroceryValidation.Validate(requestDto))
        {
            GmLogger.GetInstance()?.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        var groceryRequest = new GroceryRequest(
            user, requestDto,
            DateTime.Parse(requestDto.FromDate, null).ToUniversalTime(),
            DateTime.Parse(requestDto.ToDate, null).ToUniversalTime());

        try
        {
            await _unitOfWork.Shopping.Add(groceryRequest, user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            GmLogger.GetInstance()?.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotSaved);
        }

        GmLogger.GetInstance()?.Trace(methodName, "Grocery-Request successfully saved");
        return Ok();
    }

    [HttpGet("groceryRequest")]
    public async Task<ActionResult<GroceryResponseDto>> GetAllGroceryRequest()
    {
        const string methodName = "GET Grocery-Request";

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
        {
            GmLogger.GetInstance()?.Warn(methodName, "Invalid ModelState due to bad credentials");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequests();
        }
        catch (Exception e)
        {
            GmLogger.GetInstance()?.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address));
        }

        GmLogger.GetInstance()?.Trace(methodName, "Grocery-Response successfully mapped");
        return Ok(requests);
    }
}