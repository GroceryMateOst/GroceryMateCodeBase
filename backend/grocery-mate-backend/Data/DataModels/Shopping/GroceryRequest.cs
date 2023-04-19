using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Data.DataModels.Shopping;

public class GroceryRequest
{
    public Guid GroceryRequestId { get; set; }
    [Required] public User Client { get; set; }
    [Required] public User Contractor { get; set; }
    public Rating? Rating { get; set; }
    [Required] public ShoppingList? ShoppingList { get; set; }
    [Required] public RatingState State { get; set; }

    public GroceryRequest(ValidatedGroceryRequest validatedGroceryRequest)
    {
        Client = validatedGroceryRequest.Client;
        Contractor = validatedGroceryRequest.Contractor;
        Rating = new Rating();
        ShoppingList = new ShoppingList(validatedGroceryRequest.ShoppingList);
        State = validatedGroceryRequest.State;
    }

    public GroceryRequest()
    {
        Client = new User();
        Contractor = new User();
        Rating = new Rating();
        ShoppingList = new ShoppingList();
        State = RatingState.D;
    }
}

public enum RatingState
{
    [Description("published")] P = 1,
    [Description("accepted")] A = 2,
    [Description("fulfilled")] F = 3,
    [Description("default")] D = 99,
}