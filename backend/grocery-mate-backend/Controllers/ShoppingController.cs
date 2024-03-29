using grocery_mate_backend.BusinessLogic.Notification.Mail;
using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.Messaging;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers.EndpointControllers;

[ApiController]
[Route("api/v0/[controller]")]
public class ShoppingController : ControllerBase
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
        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            !GroceryValidation.Validate(requestDto))
        {
            GmLogger.Instance.Warn(LogMessages.REST_POST_groceryRequest,
                LogMessages.LogMessage_InvalidGroceryRequestState);
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
            GmLogger.Instance.Trace(LogMessages.REST_POST_groceryRequest, e.Message);
            return BadRequest(ResponseErrorMessages.NotSaved);
        }

        GmLogger.Instance.Trace(LogMessages.REST_POST_groceryRequest,
            LogMessages.LogMessage_GroceryRequestSaved);
        return Ok();
    }

    [HttpGet("groceryRequest")]
    public async Task<ActionResult<GroceryResponseDto>> GetAllGroceryRequest()
    {
        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist))
        {
            GmLogger.Instance.Warn(LogMessages.REST_GET_groceryRequest,
                LogMessages.LogMessage_BadCredentials);
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequests();
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address, groceryRequest.GroceryRequestId));
        }

        GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest,
            LogMessages.LogMessage_GroceryResponseMapped);
        return Ok(requests);
    }

    [HttpGet("search")]
    public async Task<ActionResult<GroceryResponseDto>> GetGroceryRequestsByZipcode([FromQuery] int zipCode)
    {
        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            AddressValidation.ValidateZipcode(zipCode))
        {
            GmLogger.Instance.Warn(LogMessages.REST_GET_search, LogMessages.LogMessage_BadCredentials);
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetAllGroceryRequestsByZipcode(zipCode);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.REST_GET_search, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = new List<GroceryResponseDto>();
        foreach (var groceryRequest in groceryRequests)
        {
            var address = await _unitOfWork.Address.FindAddressByGuid(groceryRequest.Client.AddressId);
            requests.Add(new GroceryResponseDto(groceryRequest, address, groceryRequest.GroceryRequestId));
        }

        GmLogger.Instance.Trace(LogMessages.REST_GET_search, LogMessages.LogMessage_GroceryResponseMapped);
        return Ok(requests);
    }
    
    [Authorize]
    [HttpPatch("groceryRequestState")]
    public async Task<IActionResult> UpdateRequestState(GroceryUpdateDto updatedDto)
    {
        if (!ValidationBase.ValidateModel(ModelState, Request.Headers, _unitOfWork.TokenBlacklist) &&
            !GroceryValidation.ValidateRequestState(updatedDto.RequestState))
        {
            GmLogger.Instance.Warn(LogMessages.REST_PUT_groceryRequestState,
                LogMessages.LogMessage_InvalidGroceryRequestState);
            return BadRequest(ResponseErrorMessages.InvalidRequest);
        }

        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        GroceryRequest groceryRequest;

        try
        {
            var groceryRequestId = Guid.Parse(updatedDto.GroceryRequestId);
            groceryRequest = await _unitOfWork.Shopping.GetGroceryRequestById(groceryRequestId);
            groceryRequest.State = Enum.Parse<GroceryRequestState>(updatedDto.RequestState, true);
            groceryRequest.Contractor = user;
            await _unitOfWork.CompleteAsync();

            var clientMail = groceryRequest.Client.EmailAddress;
            var contractorMail = groceryRequest.Contractor.EmailAddress;
            var clientsFullName = $"{groceryRequest.Client.FirstName} {groceryRequest.Client.SecondName}";
            var contractorsFullName = $"{groceryRequest.Contractor.FirstName} {groceryRequest.Contractor.SecondName}";
            var mailSettings = _unitOfWork.Authentication.GetMailSettings();

            switch (groceryRequest.State)
            {
                case GroceryRequestState.Accepted:
                    {
                        MailNotification.ShoppingRequestAcceptedNotificationForClient(
                            clientMail,
                            contractorsFullName,
                            mailSettings);

                        MailNotification.ShoppingRequestAcceptedNotificationForContractor(
                            contractorMail,
                            clientsFullName,
                            mailSettings);
                        break;
                    }
                case GroceryRequestState.Fulfilled:
                    {
                        MailNotification.ShoppingRequestFulfilledNotificationForClient(
                            clientMail,
                            contractorsFullName,
                            mailSettings);

                        MailNotification.ShoppingRequestFulfilledNotificationForContractor(
                            contractorMail,
                            clientsFullName,
                            mailSettings);
                        break;
                    }
            }
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.REST_PUT_groceryRequestState, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        GmLogger.Instance.Trace(LogMessages.REST_PUT_groceryRequestState,
            LogMessages.LogMessage_GroceryResponsePached);
        return Ok();
    }

    [Authorize]
    [HttpGet("groceryRequest/clientRequests")]
    public async Task<ActionResult<DetailedGroceryResponseDto>> GetAllClientRequests()
    {
        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetGroceryRequestsAsClient(user);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest_clientRequests, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        var requests = groceryRequests.Select(groceryRequest => new DetailedGroceryResponseDto(groceryRequest, user.UserId)).ToList();
        GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest_clientRequests, LogMessages.LogMessage_GroceryResponseMapped);

        return Ok(requests);
    }

    [Authorize]
    [HttpGet("groceryRequest/contractorRequests")]
    public async Task<ActionResult<DetailedGroceryResponseDto>> GetAllContractorRequests()
    {
        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        if (user == null) return BadRequest(ResponseErrorMessages.NotAuthorised);

        List<GroceryRequest> groceryRequests;
        try
        {
            groceryRequests = await _unitOfWork.Shopping.GetGroceryRequestsAsContractor(user);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest_contractorRequests, e.Message);
            return BadRequest(ResponseErrorMessages.NotFound);
        }
        
        var requests = groceryRequests.Select(groceryRequest => new DetailedGroceryResponseDto(groceryRequest, user.UserId)).ToList();

        GmLogger.Instance.Trace(LogMessages.REST_GET_groceryRequest_contractorRequests,
            LogMessages.LogMessage_GroceryResponseMapped);
        return Ok(requests);
    }
}