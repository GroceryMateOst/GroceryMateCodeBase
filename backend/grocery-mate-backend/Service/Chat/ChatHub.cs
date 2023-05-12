using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.Messaging;
using Microsoft.AspNetCore.SignalR;

namespace grocery_mate_backend.Service.Chat;

public class ChatHub : Hub
{
    private readonly IDictionary<string, UserConnection> _connections;
    private readonly IUnitOfWork _unitOfWork;
    
    public ChatHub(IDictionary<string, UserConnection> connections, IUnitOfWork unitOfWork)
    {
        _connections = connections;
        _unitOfWork = unitOfWork;
    }
    
    public override Task OnDisconnectedAsync(Exception exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            _connections.Remove(Context.ConnectionId);
        }

        return base.OnDisconnectedAsync(exception);
    }


    
    public async Task JoinChat(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ShoppingId);
        _connections[Context.ConnectionId] = userConnection;
    }

    public async Task SendMessage(string message, string receiver, string sender)
    {
        Console.WriteLine("was here with" + message + receiver + sender);
        
        if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
        {
            await Clients.Group(userConnection.ShoppingId).SendAsync("ReceiveMessage", message, receiver, sender);
        }

        Guid receiverGuid;
        Guid.TryParse(receiver, out receiverGuid);
        var receiverUser = await _unitOfWork.User.FindUserById(receiverGuid);
        Guid senderGuid;
        Guid.TryParse(sender, out senderGuid);
        var senderUser = await _unitOfWork.User.FindUserById(senderGuid);
        var messageObject = new Message(message, senderUser, receiverUser );
        
        Guid shoppingGuid;
        Guid.TryParse(userConnection.ShoppingId, out shoppingGuid);

        await _unitOfWork.Messaging.AddMessageToChat(shoppingGuid, messageObject);
    }
}