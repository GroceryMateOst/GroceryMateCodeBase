namespace grocery_mate_backend.Models.Shopping;

public class GroceryResponseBaseDto
{
    public List<ShoppingListDto> ShoppingList { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string PreferredStore { get; set; }
    public Guid GroceryRequestId { get; set; }
    
    
    public GroceryResponseBaseDto(List<ShoppingListDto> shoppingList, DateTime fromDate, DateTime toDate, string preferredStore)
    {
        ShoppingList = shoppingList;
        FromDate = fromDate;
        ToDate = toDate;
        PreferredStore = preferredStore;
    }

    public GroceryResponseBaseDto()
    {
        ShoppingList = new List<ShoppingListDto>();
        FromDate = DateTime.MinValue;
        ToDate = DateTime.MaxValue;;
        PreferredStore = string.Empty;
    }
}