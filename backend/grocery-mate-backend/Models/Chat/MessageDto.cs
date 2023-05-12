namespace grocery_mate_backend.Models.Chat;

public class MessageDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string MessageText { get; set; }
    public bool MessageRead { get; set; }
    public DateTime MessageDate { get; set; }


    public MessageDto(Guid senderId, Guid receiverId, string messageText, bool messageRead, DateTime messageDate)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        MessageText = messageText;
        MessageRead = messageRead;
        MessageDate = messageDate;
    }
}