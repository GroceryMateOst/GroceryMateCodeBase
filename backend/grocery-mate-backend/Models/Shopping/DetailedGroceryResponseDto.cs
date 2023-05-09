using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Settings;

namespace grocery_mate_backend.Models.Shopping;

public class DetailedGroceryResponseDto : GroceryResponseBaseDto
{
    public string RequestState { get; set; }
    public UserDataDto Client { get; set; }
    public UserDataDto Contractor { get; set; }
    
    public DetailedGroceryResponseDto(GroceryRequest groceryRequest)
    {
        var client = groceryRequest.Client;
        var contractor = groceryRequest.Contractor;

        client.GroceryRequestsClients = new List<GroceryRequest>();
        contractor.GroceryRequestsClients = new List<GroceryRequest>();
        client.GroceryRequestsContractor = new List<GroceryRequest>();
        contractor.GroceryRequestsContractor = new List<GroceryRequest>();

        RequestState = groceryRequest.State.ToString();
        Client = new UserDataDto(groceryRequest.Client, groceryRequest.Client.Address);
        Contractor = new UserDataDto(groceryRequest.Contractor, groceryRequest.Contractor?.Address);;

        ShoppingList = groceryRequest.ShoppingList.Items
            .Select(item => new ShoppingListDto(item.Grocery))
            .ToList();
        FromDate = groceryRequest.FromDate;
        ToDate = groceryRequest.ToDate;
        PreferredStore = groceryRequest.PreferredStore;
        GroceryRequestId = groceryRequest.GroceryRequestId;
    }
}