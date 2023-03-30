namespace grocery_mate_backend.Models;

public class GroceryRequest
{
    public Guid GroceryRequestId { get; set; }
    public User Client { get; set; }
    public User Contractor { get;  set; }
    public Rating Rating { get; set; }
    public ShoppingList ShoppingList {get; set; }
    
}