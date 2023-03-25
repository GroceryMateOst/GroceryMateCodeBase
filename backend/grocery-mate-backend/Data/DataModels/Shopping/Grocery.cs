using System.ComponentModel;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models;

public class Grocery
{
    public Guid GroceryId { get; set; }

    // public GroceryGroceries Category { get; }
    public int Size { get; set; }
    public GrocerySizeUnit Unit { get; set; }
}

public enum GrocerySizeUnit
{
    [Description("Kilogram")] Kg = 1,
    [Description("Liter")] L = 2,
}

public enum GroceryGroceries
{
    [Description("Alcoholic beverages")] Alcohol = 1,

    [Description("Non alcoholic beverages")]
    NoAlcohol = 2,

    [Description("Spices and herbs")] Spices = 3,

    [Description("Sauces and condiments")] Sauces = 4,

    [Description("Ready meals")] Ready = 5,

    [Description("Canned food")] Cannned = 6,

    [Description("Confectionery and sugar")]
    Sugar = 7,

    [Description("Oils and fats")] Oil = 8,

    [Description("Milk and dairy products")]
    Milk = 9,

    [Description("Fish and fish products")]
    Fish = 10,

    [Description("Meat and meat products")]
    Meat = 11,
    [Description("Nuts and seeds")] Nuts = 12,

    [Description("Vegetables and legumes")]
    Vegetables = 13,
    [Description("Fruit and dried fruit")] Fruit = 14,

    [Description("Potatoes and and potato products")]
    Potatoes = 15,

    [Description("Cereals and cereal products")]
    Cereals = 16,
}
