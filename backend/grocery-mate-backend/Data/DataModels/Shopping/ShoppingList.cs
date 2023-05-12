using grocery_mate_backend.Models.Shopping;


namespace grocery_mate_backend.Data.DataModels.Shopping;

public class ShoppingList
{
    public Guid ShoppingListId { get; set; }
    public List<ShoppingListItem> Items { get; set; }

    public ShoppingList(List<ShoppingListDto>shoppingListDto)
    {
        Items = new List<ShoppingListItem>();
        foreach (var grocery in shoppingListDto)
        {
            Items.Add(new ShoppingListItem(grocery.Description));
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

    public ShoppingListItem(string grocery)
    {
        Grocery = grocery;
    }
}