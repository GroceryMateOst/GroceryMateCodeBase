using System.ComponentModel;

namespace grocery_mate_backend.Models;

public class ShoppingList
{
    public Stores Store { get; }
    List<ShoppingListItem> Items { get; }
    public string Note { get; }

    public ShoppingList(Stores store, List<ShoppingListItem> items, string note)
    {
        Store = store;
        Items = items;
        Note = note;
    }
}

public class ShoppingListItem
{
    public Grocery Grocery { get; }
    public int Amount { get; }
    public string PreferedBrand { get; }
    public string Note { get; }

    public ShoppingListItem(Grocery grocery, int amount, string preferedBrand, string note)
    {
        Grocery = grocery;
        Amount = amount;
        PreferedBrand = preferedBrand;
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