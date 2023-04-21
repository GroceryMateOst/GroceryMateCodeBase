using grocery_mate_backend.Models.Shopping;


namespace grocery_mate_backend.Data.DataModels.Shopping;

public class ShoppingList
{
    public Guid ShoppingListId { get; set; }
    public List<ShoppingListItem> Items { get; set; }

    public ShoppingList(ShoppingListDto shoppingListDto)
    {
        Items = new List<ShoppingListItem>();
        foreach (var grocery in shoppingListDto.Items)
        {
            Items.Add(new ShoppingListItem(grocery));
        }
    }

    public ShoppingList()
    {
        Items = new List<ShoppingListItem>();
    }
}

public class ShoppingListItem
{
    public Guid ShoppingListItemId { get; set; }
    public string Grocery { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    public string PreferredBrand { get; set; }
    public string Note { get; set; }

    public ShoppingListItem(string grocery)
    {
        Grocery = grocery;
        Amount = 1;
        Unit = string.Empty;
        PreferredBrand = string.Empty;
        Note = string.Empty;
    }
}