using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Shopping;

namespace grocery_mate_backend.Data.DataModels.Shopping;

public class GroceryRequest
{
    public Guid GroceryRequestId { get; set; }
    [Required] public User Client { get; set; }
    [Required] public User? Contractor { get; set; }
    public Rating? Rating { get; set; }
    [Required] public ShoppingList? ShoppingList { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string Note { get; set; }
    public string PreferredStore { get; set; }
    [Required] public GroceryRequestState State { get; set; }

    public GroceryRequest(User client, GroceryRequestDto requestDto, DateTime fromDate, DateTime toDate)
    {
        Client = client;
        Rating = new Rating();
        ShoppingList = new ShoppingList(requestDto.ShoppingListDto);
        State = Enum.Parse<GroceryRequestState>(requestDto.RequestState);
        FromDate = fromDate;
        ToDate = toDate;
        Note = requestDto.note;
        PreferredStore = requestDto.PreferredStore;
    }

    public GroceryRequest()
    {
        Client = new User();
        Contractor = new User();
        Rating = new Rating();
        ShoppingList = new ShoppingList();
        FromDate = DateTime.MinValue;
        ToDate = DateTime.MinValue;
        Note = string.Empty;
        PreferredStore = string.Empty;
        State = GroceryRequestState.Unpublished;
    }
}

public enum GroceryRequestState
{
    [Description("unpublished")] Unpublished,
    [Description("published")] Published,
    [Description("accepted")] Accepted,
    [Description("fulfilled")] Fulfilled
}