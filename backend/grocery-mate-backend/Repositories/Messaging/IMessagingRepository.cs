using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.Messaging;

namespace grocery_mate_backend.Repositories.Messaging;

public interface IMessagingRepository : IGenericRepository<Chat>
{
    Task<bool> Add(Chat chat);
    Task<Chat> GetChatByGroceryId(Guid groceryId);
    Task<bool> AddMessageToChat(Guid groceryId, Message message);
    Task SetMessagesAsRead(Guid groceryId, Guid receiverId);
    Task<int> GetUnreadMessageCountByGroceryId(Guid groceryId, Guid receiverId);

}