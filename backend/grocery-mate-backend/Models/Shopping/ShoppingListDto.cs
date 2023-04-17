using System.ComponentModel.DataAnnotations;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models.Shopping;

public class ShoppingListDto
{
    public string PreferedStore { get; }

    public string Node { get; }

    [Required] public List<string> Items { get; }

    // [Required] public Dictionary<int, string> Items { get; set; }


    public ShoppingListDto()
    {
        PreferedStore = Symbols.Empty;
        Node = Symbols.Empty;
        Items = new List<string>();
    }
}