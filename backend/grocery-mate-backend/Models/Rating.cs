using System.ComponentModel;

namespace grocery_mate_backend.Models;

public class Rating
{
    public User Evaluator { get; }
    public DateTime DateTime { get; }
    public Ratings UserRating { get; }
}

public enum Ratings
{
    [Description("One")] ONE = 1,
    [Description("Two")] TWO = 2,
    [Description("Three")] THREE = 3,
    [Description("Four")] FOUR = 4,
    [Description("Five")] FIVE = 5,
}