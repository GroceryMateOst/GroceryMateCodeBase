using System.ComponentModel.DataAnnotations;


namespace grocery_mate_backend.Models.Shopping;

public class ShoppingListDto
{
    [Required] public List<string> Items { get; }
    
    public ShoppingListDto()
    {
        Items = new List<string>();
    }
}