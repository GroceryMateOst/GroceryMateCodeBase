using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.Messaging;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("groceryRequest")]
    public async Task<ActionResult<GroceryRequestDto>> PostGroceryRequest(GroceryRequestDto requestDto)
    {
        const string methodName = "POST Grocery-Request";

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            !GroceryValidation.Validate(requestDto))
        {
            GmLogger.Instance.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        var groceryRequest = new GroceryRequest(
            user, requestDto,
            DateTime.Parse(requestDto.FromDate, null).ToUniversalTime(),
            DateTime.Parse(requestDto.ToDate, null).ToUniversalTime());
        var chat = new Chat(groceryRequest);

        try
        {
            await _unitOfWork.Shopping.Add(groceryRequest, user);
            await _unitOfWork.Messaging.Add(chat);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotSaved);
        }

        GmLogger.Instance.Trace(methodName, "Grocery-Request successfully saved");
        return Ok();
    }

    [HttpGet("groceryRequest")]
    public async Task<ActionResult<GroceryResponseDto>> GetAllGroceryRequest()
    {
        const string methodName = "GET Grocery-Request";

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
        {
            GmLogger.Instance.Warn(methodName, "Invalid ModelState due to bad credentials");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequests();
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address, groceryRequest.GroceryRequestId));
        }

        GmLogger.Instance.Trace(methodName, "Grocery-Response successfully mapped");
        return Ok(requests);
    }
    
    [HttpGet("Search")]
    public async Task<ActionResult<GroceryResponseDto>> GetGroceryRequestsByZipcode([FromQuery] int zipCode)
    {
        const string methodName = "GET Grocery-Request by Zipcode";

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            AddressValidation.ValidateZipcode(zipCode))
        {
            GmLogger.Instance.Warn(methodName, "Invalid ModelState due to bad credentials");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequestsByZipcode(zipCode);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address, groceryRequest.GroceryRequestId));
        }

        GmLogger.Instance.Trace(methodName, "Grocery-Response successfully mapped");
        return Ok(requests);
    }


    [Authorize]
    [HttpPut("groceryRequestState")]
    public async Task<IActionResult> UpdateRequestState(string requestId, string state)
    {
        const string methodName = "PATCH Grocery-Request-state";

        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            !GroceryValidation.ValidateRequestState(state))
        {
            GmLogger.Instance.Warn(methodName, "GroceryRequestState is invalid");
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        GroceryRequest groceryRequest;

        try
        {
            var groceryRequestId = Guid.Parse(requestId);
            groceryRequest = await _unitOfWork.Shopping.GetById(groceryRequestId);
            groceryRequest.State = Enum.Parse<GroceryRequestState>(state, true);
            groceryRequest.Contractor = user;
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        GmLogger.Instance.Trace(methodName, "Grocery-Response successfully patched");
        return Ok();
    }

    [Authorize]
    [HttpGet("groceryRequest/clientRequests")]
    public async Task<ActionResult<DetailedGroceryResponseDto>> GetAllClientRequests()
    {
        const string methodName = "GET Grocery-Request from a client";

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetGroceryRequestsAsClient(user);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = groceryRequests.Select(groceryRequest => new DetailedGroceryResponseDto(groceryRequest, user.UserId)).ToList();


        GmLogger.Instance.Trace(methodName, "Grocery-Response successfully mapped");
        return Ok(requests);
    }

    [Authorize]
    [HttpGet("groceryRequest/contractorRequests")]
    public async Task<ActionResult<DetailedGroceryResponseDto>> GetAllContractorRequests()
    {
        const string methodName = "GET Grocery-Request from a contractor";
        
        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetGroceryRequestsAsContractor(user);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(methodName, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = groceryRequests.Select(groceryRequest => new DetailedGroceryResponseDto(groceryRequest, user.UserId))
            .ToList();

        GmLogger.Instance.Trace(methodName, "Grocery-Response successfully mapped");
        return Ok(requests);
    }
}