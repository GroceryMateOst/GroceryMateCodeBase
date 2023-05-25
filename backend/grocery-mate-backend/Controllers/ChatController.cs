using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Models.Chat;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility;
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
        var chat = await _unitOfWork.Messaging.GetChatByGroceryId(groceryId);
        if (chat == null)
        {
            GmLogger.Instance.Warn(LogMessages.REST_GET_chat, LogMessages.LogMessage_NoChatMessage);
            return BadRequest(ResponseErrorMessages.NotFound);
        }

        return Ok(new ChatDto(chat));
    }
    
    [Authorize]
    [HttpPut("read")]
    public async Task<IActionResult> UpdateMessagesRead(Guid groceryId)
    {
        var user = await UserService.GetAuthenticatedUser(User.Identity?.Name, _unitOfWork);
        await _unitOfWork.Messaging.SetMessagesAsRead(groceryId, user.UserId);
        GmLogger.Instance.Trace(LogMessages.REST_PUT_MessageReadStatus, LogMessages.LogMessage_ReadChatMessage + groceryId);
        return Ok();
    }

}
