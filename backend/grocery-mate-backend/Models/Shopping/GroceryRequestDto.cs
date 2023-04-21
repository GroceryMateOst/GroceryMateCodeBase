using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryRequestDto
{
    [Required] public ShoppingListDto ShoppingListDto { get; set; }
    public string PreferredStore { get; set; }
    [Required] public string RequestState { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }
    public string note { get; set; }

    public GroceryRequestDto()
    {
        ShoppingListDto = new ShoppingListDto();
        PreferredStore = string.Empty;
        RequestState = string.Empty;
        fromDate = string.Empty;
        toDate = string.Empty;
        note = string.Empty;
    }
}