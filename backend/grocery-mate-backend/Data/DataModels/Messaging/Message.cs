using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Data.DataModels.Messaging;

public class Message
{
    public Guid MessageId { get; set; }
    public string MessageText { get; set; }
    [ForeignKey("Sender")]
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    [ForeignKey("Receiver")]
    public Guid ReceiverId { get; set; }
    [Required] 
    public User Receiver { get; set; }
    public bool MessageRead { get; set; }
    public DateTime MessageDate { get; set; }

    public Message(string messageText, User sender, User receiver)
    {
        MessageText = messageText;
        Sender = sender;
        Receiver = receiver;
        MessageRead = false;
        MessageDate = DateTime.UtcNow;
    }
    
    public Message()
    {
        MessageText = String.Empty;
        Sender = new User();
        Receiver = new User();
        MessageRead = false;
        MessageDate = DateTime.UtcNow;
    }

}