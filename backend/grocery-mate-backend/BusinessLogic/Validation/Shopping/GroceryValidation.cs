using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation.Shopping;

public static class GroceryValidation
{
    private delegate bool GroceryPredicate<in T>(T item);
    
    public static bool Validate(GroceryRequestDto requestDto)
    {
        return ValidateGroceryList(requestDto.GroceryList) &&
               ValidateRequestState(requestDto.RequestState) &&
               ValidateDateTime(requestDto.FromDate) &&
               ValidateDateTime(requestDto.ToDate);
    }
    
    public static bool ValidateRequestState(string requestState)
    {
        return Validate(requestState,
            "ValidateRequestState",
            "RequestState is incorrect",
            item => item is "published");
    }

    public static bool ValidateGroceryList(List<ShoppingListDto> requestDto)
    {
        return Validate(requestDto,
            "ValidateGroceryList",
            "Shopping list is empty",
            item => item.All(groceryList => !groceryList.Description.IsNullOrEmpty()));
    }

    public static bool ValidateDateTime(string date)
    {
        return Validate(date,
            "ValidateDateTime",
            "Invalid date format",
            item => DateTime.TryParse(item, out _));
    }

    private static bool Validate<T>(T thing, string methodName, string errorMsg, GroceryPredicate<T> predicate)
    {
        if (predicate(thing)) return true;
        GmLogger.Instance.Warn(methodName, errorMsg);
        return false;
    }
}