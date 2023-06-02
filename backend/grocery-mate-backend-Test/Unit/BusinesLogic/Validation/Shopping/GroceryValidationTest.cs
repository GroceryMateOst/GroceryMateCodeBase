using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Models.Shopping;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation.Shopping;

public class GroceryValidationTests
{
    [Test]
    public void ValidateRequestState_ValidRequestState_ReturnsTrue()
    {
        // Arrange
        const string requestStatePublished = "published";
        const string requestStateAccepted = "accepted";
        const string requestStateFulfilled = "fulfilled";

        // Act
        var resultPublished = GroceryValidation.ValidateRequestState(requestStatePublished);
        var resultAccepted = GroceryValidation.ValidateRequestState(requestStateAccepted);
        var resultFulfilled = GroceryValidation.ValidateRequestState(requestStateFulfilled);
       
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultPublished, Is.True);
            Assert.That(resultAccepted, Is.True);
            Assert.That(resultFulfilled, Is.True);
        });
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

    [Test]
    public void ValidateGroceryList_ValidGroceryList_ReturnsTrue()
    {
        // Arrange
        List<ShoppingListDto> list = new List<ShoppingListDto> { new("sush"), new("woota") };
        
        // Act
        var result = GroceryValidation.ValidateGroceryList(list);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateGroceryList_ValidGroceryList_ReturnsFalse()
    {
        // Arrange
        List<ShoppingListDto> list = new List<ShoppingListDto> { new(""), new("woota") };
        
        // Act
        var result = GroceryValidation.ValidateGroceryList(list);

        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void ValidateGroceryList_ValidDateTime_ReturnsTrue()
    {
        // Arrange
        string date = "2023-04-21T09:19:26.371Z";
        
        // Act
        var result = GroceryValidation.ValidateDateTime(date);

        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ValidateGroceryList_ValidDateTime_ReturnsFalse()
    {
        // Arrange
        string date = "2023-04-31:26.371";
        
        // Act
        var result = GroceryValidation.ValidateDateTime(date);

        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void Validate_ValidRequestDto_ReturnsTrue()
    {
        // Arrange
        var requestDto = new GroceryRequestDto
        {
            GroceryList = new List<ShoppingListDto> { new("sushi"), new("umami") },
            RequestState = "published",
            FromDate = "2023-04-01T09:00:00.000Z",
            ToDate = "2023-04-30T17:00:00.000Z"
        };

        // Act
        var result = GroceryValidation.Validate(requestDto);

        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void Validate_InvalidRequestDto_ReturnsTrue()
    {
        // Arrange
        var requestDto = new GroceryRequestDto
        {
            GroceryList = new List<ShoppingListDto> { new(""), new("umami") },
            RequestState = "asdfasdf",
            FromDate = "2023sd-04-01T09:00:00.000Z",
            ToDate = "2023af-04-30T17:00:00.000Z"
        };

        // Act
        var result = GroceryValidation.Validate(requestDto);

        // Assert
        Assert.That(result, Is.False);
    }
}