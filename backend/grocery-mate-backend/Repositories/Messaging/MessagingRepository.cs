using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Messaging;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Repositories.Messaging;

public class MessagingRepository : GenericRepository<Chat>, IMessagingRepository 
{
    private readonly GroceryContext _context; 
    
    public MessagingRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> Add(Chat chat)
    {
        _context.Attach(chat);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Chat> GetChatByGroceryId(Guid groceryId)
    {
        return await _context.Chat
            .Include(chat => chat.Messages)
            .FirstOrDefaultAsync(chat => groceryId == chat.GroceryRequestId);
    }

    public async Task<bool> AddMessageToChat(Guid groceryId, Message message)
    {
        var chat = await GetChatByGroceryId(groceryId);
        chat.Messages.Add(message);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<int> GetUnreadMessageCountByGroceryId(Guid groceryId, Guid receiverId)
    {
        var chat = await GetChatByGroceryId(groceryId);

        if (chat == null)
        {
            return 0;
        }

        int unreadMessageCount = chat.Messages.Count(m => m.ReceiverId == receiverId && !m.MessageRead);
        return unreadMessageCount;
    }

    
    public async Task SetMessagesAsRead(Guid groceryId, Guid receiverId)
    {
        var chat = await GetChatByGroceryId(groceryId);
        if (chat == null)
        {
            return; 
        }

        foreach (var message in chat.Messages)
        {
            if (message.ReceiverId == receiverId)
            {
                message.MessageRead = true;
            }
        }

        await _context.SaveChangesAsync();
    }

}