using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryResponseDto : GroceryResponseBaseDto
{
    public string FirstName { get; set; }
    public string City { get; set; }
    public decimal Distance { get; set; }

    public GroceryResponseDto(GroceryRequest groceryRequest, Address? address, Guid groceryRequestId)
    {
        FirstName = groceryRequest.Client.FirstName;
        City = address != null ? address.City : "";
        ShoppingList = groceryRequest.ShoppingList.Items
            .Select(item => new ShoppingListDto(item.Grocery))
            .ToList();
        FromDate = groceryRequest.FromDate;
        ToDate = groceryRequest.ToDate;
        PreferredStore = groceryRequest.PreferredStore;
        GroceryRequestId = groceryRequestId;
        Distance = groceryRequest.Distance;
        Note = groceryRequest.Note;
    }
}