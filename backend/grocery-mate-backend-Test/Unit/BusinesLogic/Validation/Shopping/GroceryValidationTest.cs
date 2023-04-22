using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.Models.Shopping;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation.Shopping;

public class GroceryValidationTests
{
    [Test]
    public void ValidateRequestState_ValidRequestState_ReturnsTrue()
    {
        // Arrange
        const string requestState = "published";

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

        bool asdf = !"".IsNullOrEmpty();
        
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
}