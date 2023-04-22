using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation.Shopping;

public class GroceryValidationTests
{
    [Test]
    public void ValidateRequestState_ValidRequestState_ReturnsTrue()
    {
        // Arrange
        const string requestState = "unpublished";

        // Act
        var result = GroceryValidation.ValidateRequestState(requestState);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateRequestState_InvalidRequestState_ReturnsFalse()
    {
        // Arrange
        const string requestState = "invalid_state";

        // Act
        var result = GroceryValidation.ValidateRequestState(requestState);

        // Assert
        Assert.That(result, Is.False);
    }
}
