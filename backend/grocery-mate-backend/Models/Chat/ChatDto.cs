using grocery_mate_backend.Data.DataModels.Messaging;

namespace grocery_mate_backend.Models.Chat;

public class ChatDto
{
    public List<MessageDto> Messages { get; set; }

    public ChatDto(Data.DataModels.Messaging.Chat chat)
    {
        Messages = chat.Messages
            .Select(message => new MessageDto(message.SenderId, message.ReceiverId, message.MessageText, message.MessageRead, message.MessageDate))
            .ToList();
    }
}