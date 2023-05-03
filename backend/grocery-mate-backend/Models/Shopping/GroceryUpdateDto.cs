using System.ComponentModel.DataAnnotations;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryUpdateDto
{
    [Required] public string GroceryRequestId  { get; set; }

    [Required] public string RequestState { get; set; }

    public GroceryUpdateDto()
    {
        GroceryRequestId = Guid.Empty.ToString();
        RequestState = string.Empty;
    }

    public GroceryUpdateDto(string groceryRequestId, string requestState)
    {
        GroceryRequestId = groceryRequestId;
        RequestState = requestState;
    }
}