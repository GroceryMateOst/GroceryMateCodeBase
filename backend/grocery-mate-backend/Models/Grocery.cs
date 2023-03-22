using System.ComponentModel;
using grocery_mate_backend.Sandbox;

namespace grocery_mate_backend.Models;

public class Grocery
{
    public GroceryGroceries Category { get; }
    public int Size { get; }
    public int Unit { get; }

    public string FullSize
    {
        get { return Size + Symbols.Space + Unit; }
    }

    public Grocery(GroceryGroceries category, int size, int unit)
    {
        Category = category;
        Size = size;
        Unit = unit;
    }
}

public enum GroceryGroceries
{
    [Description("Alcoholic beverages")] ALCOHOL = 1,

    [Description("Non alcoholic beverages")]
    NO_ALCOHOL = 2,

    [Description("Spices and herbs")] SPICES = 3,

    [Description("Sauces and condiments")] SAUCES = 4,

    [Description("Ready meals")] READY = 5,

    [Description("Canned food")] CANNNED = 6,

    [Description("Confectionery and sugar")]
    SUGAR = 7,

    [Description("Oils and fats")] OIL = 8,

    [Description("Milk and dairy products")]
    MILK = 9,

    [Description("Fish and fish products")]
    FISH = 10,

    [Description("Meat and meat products")]
    MEAT = 11,
    [Description("Nuts and seeds")] NUTS = 12,

    [Description("Vegetables and legumes")]
    VEG = 13,
    [Description("Fruit and dried fruit")] FRUIT = 14,

    [Description("Potatoes and and potato products")]
    POT = 15,

    [Description("Cereals and cereal products")]
    CER = 16,
}

public enum GrocerySizeUnit
{
    [Description("Milliliter")] ML = 1,
    [Description("Gram")] G = 2,
}