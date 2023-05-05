using grocery_mate_backend.Models.Shopping;
using grocery_mate_backend.Utility.Log;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation;

public abstract class GroceryValidation : ValidationBase
{
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
            "RequestState is incorrect",
            item => item is "published" or "accepted" or "fulfilled");
    }

    public static bool ValidateGroceryList(List<ShoppingListDto> requestDto)
    {
        return Validate(requestDto,
            "Shopping list is empty",
            item => item.All(groceryList => !groceryList.Description.IsNullOrEmpty()));
    }

    public static bool ValidateDateTime(string date)
    {
        return Validate(date,
            "Invalid date format",
            item => DateTime.TryParse(item, out _));
    }
}