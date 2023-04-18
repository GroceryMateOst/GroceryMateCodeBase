using System.ComponentModel;

namespace grocery_mate_backend.Data.DataModels.UserManagement;

public class Rating
{
    public Guid RatingId { get; set; }
    public User? Evaluator { get; set;}
    public DateTime DateTime { get; set;}
    public Ratings UserRating { get; set;}

    public Rating(User evaluator, DateTime dateTime, Ratings userRating)
    {
        Evaluator = evaluator;
        DateTime = dateTime;
        UserRating = userRating;
    }

    public Rating()
    {
        Evaluator = new User();
        DateTime = DateTime.MinValue;
        UserRating = Ratings.Default;
    }
}

public enum Ratings
{
    [Description("One")] One = 1,
    [Description("Two")] Two = 2,
    [Description("Three")] Three = 3,
    [Description("Four")] Four = 4,
    [Description("Five")] Five = 5,
    [Description("Default")] Default = -1,
}