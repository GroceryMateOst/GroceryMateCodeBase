using grocery_mate_backend;
namespace grocery_mate_backend_UnitTest;

public class ExampleTest
{

    private Example reservation;

    [SetUp]
    public void Setup()
    {
        var reservation = new Example();
    }

    
    [Test]
    public void CanBeCanceled_UserIsAdmin_true() //Test-Method-Name: NameOfTestedMethod_Scenario_ExpectedBehavior();
    {
        //Arrange
        var reservation = new Example();
        
        // Act
        var result = reservation.CanBeCanceled(new User{IsAdmin = true});

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void CanBeCanceled_SameUserCancelingReservation_true()
    {
        //Arrange
        var user = new User();
        var reservation = new Example{MadeBy = user};

        // Act
        var result = reservation.CanBeCanceled(user);

        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void CanBeCanceled_DifferentUserCancelingReservation_false()
    {
        //Arrange
        var user = new User();
        var reservation = new Example{MadeBy = user};

        // Act
        var result = reservation.CanBeCanceled(new User{IsAdmin = false});

        // Assert
        Assert.IsFalse(result);
    }
}