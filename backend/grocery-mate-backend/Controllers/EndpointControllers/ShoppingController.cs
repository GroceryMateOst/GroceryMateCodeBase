using grocery_mate_backend.BusinessLogic.Validation.Authentication;
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
    public async Task<ActionResult<GroceryRequestDto>> PostGroceryRequest([FromQuery] GroceryRequestDto requestDto
    )
    {
        const string methodName = "POST Grocery-Request";

        if (!AuthenticationValidation.ValidateModelState(ModelState, methodName))
            return BadRequest("Invalid Request!");

        if (!UserValidation.ValidateUserMail(requestDto.ClientMail, methodName) ||
            !UserValidation.ValidateUserMail(requestDto.ContractorMail, methodName))
            return BadRequest("Invalid Mail-Address!");

        var user = await _unitOfWork.User.FindUserByMail(requestDto.ClientMail);
        var contractor = await _unitOfWork.User.FindUserByMail(requestDto.ContractorMail);

        if (!UserValidation.ValidateUser(user, methodName) ||
            !UserValidation.ValidateUser(contractor, methodName))
            return BadRequest("Client or Contractor not found!");

        try
        {
            GroceryValidation.ValidateGroceryList(requestDto, user, contractor);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var validatedGroceryRequest = GroceryValidation.CreateValidatedGroceryRequest(requestDto, user, contractor);

        GroceryRequest? groceryRequest = new GroceryRequest();
        switch (requestDto.RequestState?.ToLower())
        {
            case "published":
            {
                groceryRequest = new GroceryRequest(validatedGroceryRequest);
                groceryRequest.State = RatingState.P;
                break;
            }
            case "accepted":
            {
                groceryRequest =
                    await _unitOfWork.Shopping.FindGroceryRequest(user.EmailAddress, contractor.EmailAddress);
                if (!GroceryValidation.ValidateGroceryRequest(groceryRequest, methodName))
                    groceryRequest.State = RatingState.A;
                break;
            }
            case "fulfilled":
            {
                groceryRequest =
                    await _unitOfWork.Shopping.FindGroceryRequest(user.EmailAddress, contractor.EmailAddress);
                if (!GroceryValidation.ValidateGroceryRequest(groceryRequest, methodName))
                    groceryRequest.State = RatingState.F;
                break;
            }
        }

        try
        {
            _unitOfWork.Shopping.Add(groceryRequest);
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