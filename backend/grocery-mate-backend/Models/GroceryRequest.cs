namespace grocery_mate_backend.Models;

public class GroceryRequest
{
    public User Client { get; }
    public User Contractor { get; }
    public Location Location { get; }
    public Rating Rating { get; set; }
    public ShoppingList ShoppingList {get; set; }

    public GroceryRequest(User client, User contractor, Location location)
    {
        Client = client;
        Contractor = contractor;
        Location = location;
    }

    public GroceryRequest(User client, User contractor, Location location, ShoppingList shoppingList)
    {
        Client = client;
        Contractor = contractor;
        Location = location;
        ShoppingList = shoppingList;
    }
}