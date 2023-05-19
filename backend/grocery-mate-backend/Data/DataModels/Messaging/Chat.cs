using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using grocery_mate_backend.Data.DataModels.Shopping;

namespace grocery_mate_backend.Data.DataModels.Messaging;

public class Chat
{
    public Guid ChatId { get; set; }
    public List<Message> Messages { get; set; }
    [Required] 
    
    [ForeignKey("GroceryRequest")]
    public Guid GroceryRequestId { get; set; }
    [Required]
    public GroceryRequest GroceryRequest { get; set; }

    

    public Chat(GroceryRequest groceryRequest)
    {
        Messages = new List<Message>();
        GroceryRequest = groceryRequest;
    }
    
    public Chat()
    {
        Messages = new List<Message>();
        GroceryRequest = new GroceryRequest();
    }
}