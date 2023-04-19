using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models.Shopping;

public class GroceryRequestDto
{
    [Required] public string ClientMail { get; set; }
    [Required] public string ContractorMail { get; set; }
    public ShoppingListDto ShoppingList { get; set; }
    [Required] public string RequestState { get; set; }

    public GroceryRequestDto()
    {
        ClientMail = Symbols.Empty;
        ContractorMail = Symbols.Empty;
        ShoppingList = new ShoppingListDto();
        RequestState = Symbols.Empty;
    }
}