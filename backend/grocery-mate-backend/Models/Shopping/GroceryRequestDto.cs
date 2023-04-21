using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryRequestDto
{
    public List<ShoppingListDto> GroceryList { get; set; }
    public string PreferredStore { get; set; }
    public string RequestState { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string Note { get; set; }

    public GroceryRequestDto()
    {
        GroceryList = new List<ShoppingListDto>();
        PreferredStore = string.Empty;
        RequestState = string.Empty;
        FromDate = string.Empty;
        ToDate = string.Empty;
        Note = string.Empty;
    }
}