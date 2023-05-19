using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models.Chat;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("api/v0/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public ChatController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ChatDto>> GetChat(Guid groceryId)
    {
        const string methodName = "REST Get Chat";

        var chat = await _unitOfWork.Messaging.GetChatByGroceryId(groceryId);
        if (chat == null)
        {
            GmLogger.Instance.Warn(methodName, "There is no Chat for this GroceryReqeust");
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        return Ok(new ChatDto(chat));
    }
    
    [Authorize]
    [HttpPut("read")]
    public async Task<IActionResult> UpdateMesasgesRead(Guid groceryId)
    {
        
        const string methodName = "REST Update Messages Read";
        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        await _unitOfWork.Messaging.SetMessagesAsRead(groceryId, user.UserId);
        return Ok();
    }

}
