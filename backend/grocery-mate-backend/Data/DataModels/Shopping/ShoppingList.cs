using System.ComponentModel;

namespace grocery_mate_backend.Models;

public class ShoppingList
{
    public Guid ShoppingListId { get; set; }
    public Stores Store { get; set; }
    List<ShoppingListItem> Items { get; set; }
    public string Note { get; set; }
}

public class ShoppingListItem
{
    public Guid ShoppingListItemId { get; set; }
    public Grocery Grocery { get; set; }
    public int Amount { get; set; }
    public string PreferredBrand { get; set; }
    public string Note { get; set; }

    public ShoppingListItem(Grocery grocery, int amount, string preferredBrand, string note)
    {
        Grocery = grocery;
        Amount = amount;
        PreferredBrand = preferredBrand;
        Note = note;
    }
}

public enum Stores
{
    [Description("Migros")] M = 1,
    [Description("Coop")] C = 2,
    [Description("Aldi")] A = 3,
    [Description("Lidl")] L = 4,
    [Description("Spar")] S = 5,
    [Description("Volg")] V = 6,
}