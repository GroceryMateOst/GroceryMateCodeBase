using System.ComponentModel.DataAnnotations;


namespace grocery_mate_backend.Models.Shopping;

public class ShoppingListDto
{
    public string Description { get; set; }
    
    public ShoppingListDto()
    {
         Description= string.Empty;
    }
}