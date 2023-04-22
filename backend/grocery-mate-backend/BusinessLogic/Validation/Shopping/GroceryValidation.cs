using grocery_mate_backend.Models.Shopping;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.BusinessLogic.Validation.Shopping;

public static class GroceryValidation
{
    public static bool ValidateRequestState(string requestState)
    {
        return requestState is "unpublished" or "published";
    }
    
    public static bool ValidateGroceryList(List<ShoppingListDto> requestDto)
    {
        return requestDto.All(groceryList => groceryList.Description.IsNullOrEmpty());
    }
}
