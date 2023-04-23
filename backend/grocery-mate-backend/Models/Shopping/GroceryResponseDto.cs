using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryResponseDto
{
    public string FirstName { get; set; }
    public string City { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string PreferredStore { get; set; }

    public GroceryResponseDto(GroceryRequest groceryRequest, Address? address)
    {
        FirstName = groceryRequest.Client.FirstName;
        City = address != null ? address.City : "";
        ShoppingList = groceryRequest.ShoppingList.Items
            .Select(item => new ShoppingListDto(item.Grocery))
            .ToList();
        FromDate = groceryRequest.FromDate;
        ToDate = groceryRequest.ToDate;
        PreferredStore = groceryRequest.PreferredStore;
    }
}