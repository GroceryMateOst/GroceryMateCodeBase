using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;

namespace grocery_mate_backend.Models.Shopping;

public class DetailedGroceryResponseDto : GroceryResponseBaseDto
{
    public string RequestState { get; set; }
    public User Client { get; set; }
    public User Contractor { get; set; }


    public DetailedGroceryResponseDto(List<ShoppingListDto> shoppingList, DateTime fromDate, DateTime toDate, string preferredStore, Guid groceryRequestId, string requestState, User client, User contractor) : base(shoppingList, fromDate, toDate, preferredStore)
    {
        GroceryRequestId = groceryRequestId;
        RequestState = requestState;
        Client = client;
        Contractor = contractor;
    }

    public DetailedGroceryResponseDto(GroceryRequest groceryRequest)
    { 
        var client = groceryRequest.Client;
        var contractor = groceryRequest.Contractor;

        client.GroceryRequestsClients = new List<GroceryRequest>();
        contractor.GroceryRequestsClients = new List<GroceryRequest>();
        client.GroceryRequestsContractor = new List<GroceryRequest>();
        contractor.GroceryRequestsContractor = new List<GroceryRequest>();
        
        RequestState = groceryRequest.State.ToString();
        Client = client;
        Contractor = contractor;

        ShoppingList = groceryRequest.ShoppingList.Items
            .Select(item => new ShoppingListDto(item.Grocery))
            .ToList();
        FromDate = groceryRequest.FromDate;
        ToDate = groceryRequest.ToDate;
        PreferredStore = groceryRequest.PreferredStore;
        GroceryRequestId = groceryRequest.GroceryRequestId;
    }
}