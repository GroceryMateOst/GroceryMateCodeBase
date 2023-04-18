using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.BusinessLogic.Validation.Shopping;

public static class GroceryValidation
{
    public static bool ValidateGroceryList(GroceryRequestDto requestDto, User client,
        User contractor)
    {
        var requestStateLower = requestDto.RequestState.ToLower();

        if (client.Equals(contractor))
        {
            throw new InvalidGroceryRequestException("User may not be client!");
        }

        if (requestStateLower is not ("published" or "accepted" or "fulfilled"))
        {
            throw new InvalidGroceryRequestException("Invalid Request-State!");
        }

        return true;
    }

    public static ValidatedGroceryRequest CreateValidatedGroceryRequest(GroceryRequestDto requestDto, User client,
        User contractor)
    {
        var ratingState = RatingState.D;
        var requestStateLower = requestDto.RequestState.ToLower();

        ratingState = requestStateLower switch
        {
            "published" => RatingState.P,
            "accepted" => RatingState.A,
            "fulfilled" => RatingState.F,
            _ => ratingState
        };

        return new ValidatedGroceryRequest(
            client,
            contractor,
            ValidateShoppingList(requestDto.ShoppingList),
            ratingState
        );
    }

    public static bool ValidateGroceryRequest(GroceryRequest? groceryRequest, string methodName)
    {
        if (groceryRequest != null) return true;
        GmLogger.GetInstance()?.Warn(methodName, "Invalid Grocery-Request!");
        return false;
    }

    private static ValidatedShoppingList ValidateShoppingList(ShoppingListDto shoppingListDto)
    {
        var items = new ValidatedShoppingList();
        if (shoppingListDto.Items.Any(item => item.Equals(null) || item.Length > 125))
        {
            throw new InvalidGroceryRequestException("Invalid shopping list position!");
        }

        items.ShoppingList = shoppingListDto.Items;
        return items;
    }
}

public class ValidatedShoppingList
{
    public List<string> ShoppingList { get; set; } = new List<string>();

    public void Add(string item)
    {
        ShoppingList.Add(item);
    }
}

public class ValidatedGroceryRequest
{
    public User Client { get; set; }
    public User Contractor { get; set; }
    public ValidatedShoppingList ShoppingList { get; set; }
    public RatingState State { get; set; }

    public ValidatedGroceryRequest(User client, User contractor, ValidatedShoppingList shoppingList, RatingState state)
    {
        Client = client;
        Contractor = contractor;
        ShoppingList = shoppingList;
        State = state;
    }
}

public class InvalidGroceryRequestException : Exception
{
    public InvalidGroceryRequestException(string methodName) : base("Invalid Grocery-Request!")
    {
        GmLogger.GetInstance()?.Trace(methodName, "Invalid Grocery-Request!");
    }

    public InvalidGroceryRequestException(string message, string methodName) : base(message)
    {
        GmLogger.GetInstance()?.Trace(methodName, message);
    }
}