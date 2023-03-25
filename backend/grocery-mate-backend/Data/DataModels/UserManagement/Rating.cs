using System.ComponentModel;

namespace grocery_mate_backend.Models;

public class Rating
{
    public Guid RatingId { get; set; }
    public User Evaluator { get; }
    public DateTime DateTime { get; }
    public Ratings UserRating { get; }
}

public enum Ratings
{
    [Description("One")] One = 1,
    [Description("Two")] Two = 2,
    [Description("Three")] Three = 3,
    [Description("Four")] Four = 4,
    [Description("Five")] Five = 5,
}