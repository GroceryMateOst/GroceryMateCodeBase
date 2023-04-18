using System.ComponentModel;
using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Data.DataModels.Shopping;

public class ShoppingList
{
    public Guid ShoppingListId { get; set; }
    public Stores PreferredStore { get; set; }
    public List<ShoppingListItem> Items { get; set; }
    public string Note { get; set; }

    public ShoppingList(ValidatedShoppingList validatedShoppingList)
    {
        PreferredStore = Stores.D;
        Note = Symbols.Empty;
        Items = new List<ShoppingListItem>();
        foreach (var shoppingListPosition in validatedShoppingList.ShoppingList)
        {
            Items.Add(new ShoppingListItem(shoppingListPosition));
        }
    }

    public ShoppingList()
    {
        PreferredStore = Stores.D;
        Items = new List<ShoppingListItem>();
        Note = Symbols.Empty;
    }
}

public class ShoppingListItem
{
    public Guid ShoppingListItemId { get; set; }
    public Grocery Grocery { get; set; }
    public int Amount { get; set; }
    public string PreferredBrand { get; set; }
    public string Note { get; set; }

    public ShoppingListItem(string position)
    {
        Grocery = new Grocery(position);
        Amount = 1;
        PreferredBrand = Symbols.Empty;
        Note = Symbols.Empty;
    }

    public ShoppingListItem()
    {
        Grocery = new Grocery();
        Amount = 1;
        PreferredBrand = Symbols.Empty;
        Note = Symbols.Empty;
    }
}

public enum Stores
{
    [Description("Default")] D = 0,
    [Description("Migros")] M = 1,
    [Description("Coop")] C = 2,
    [Description("Aldi")] A = 3,
    [Description("Lidl")] L = 4,
    [Description("Spar")] S = 5,
    [Description("Volg")] V = 6,
}