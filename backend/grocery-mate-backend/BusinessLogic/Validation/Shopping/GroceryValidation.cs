using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;

namespace grocery_mate_backend.BusinessLogic.Validation.Shopping;

public static class GroceryValidation
{
    public static bool ValidateRequestState(string requestState)
    {
        return requestState is "unpublished" or "published";
    }
}

// public class ValidatedShoppingList
// {
//     public List<string> ShoppingList { get; set; }
//
//     public ValidatedShoppingList(ShoppingListDto shoppingList)
//     {
//         ShoppingList = shoppingList.Description;
//     }
// }
//
// public class ValidatedGroceryRequest
// {
//     public User Client { get; set; }
//     public User? Contractor { get; set; }
//     public ValidatedShoppingList ShoppingList { get; set; }
//     
//     public string PreferredStore { get; set; }
//     public GroceryRequestState State { get; set; }
//
//     public ValidatedGroceryRequest(User client, ValidatedShoppingList shoppingList, string preferredStore,
//         GroceryRequestState state)
//     {
//         Client = client;
//         ShoppingList = shoppingList;
//         PreferredStore = preferredStore;
//         State = state;
//     }
// }
//
// public class InvalidGroceryRequestException : Exception
// {
//     public InvalidGroceryRequestException(string methodName) : base("Invalid Grocery-Request!")
//     {
//         GmLogger.GetInstance()?.Trace(methodName, "Invalid Grocery-Request!");
//     }
//
//     public InvalidGroceryRequestException(string message, string methodName) : base(message)
//     {
//         GmLogger.GetInstance()?.Trace(methodName, message);
     // }
// }