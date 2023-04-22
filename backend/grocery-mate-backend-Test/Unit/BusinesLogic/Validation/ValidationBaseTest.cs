using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation;

public class ValidationBaseTests
{
    [Test]
    public void ValidateModelState_ValidModelState_ReturnsTrue()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        const string methodName = "TestMethod";

        // Act
        var result = ValidationBase.ValidateModelState(modelState, methodName);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateModelState_InvalidModelState_ReturnsFalse()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("key", "error message");
        const string methodName = "TestMethod";

        // Act
        var result = ValidationBase.ValidateModelState(modelState, methodName);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateAddress_ValidAddress_ReturnsTrue()
    {
        // Arrange
        var address = new Address {AddressId = Guid.NewGuid()};
        const string methodName = "TestMethod";

        // Act
        var result = ValidationBase.ValidateAddress(address, methodName);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateAddress_NullAddress_ReturnsFalse()
    {
        // Arrange
        Address? address = null;
        const string methodName = "TestMethod";

        // Act
        var result = ValidationBase.ValidateAddress(address, methodName);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateAddress_EmptyGuidAddress_ReturnsFalse()
    {
        // Arrange
        var address = new Address {AddressId = Guid.Empty};
        const string methodName = "TestMethod";

        // Act
        var result = ValidationBase.ValidateAddress(address, methodName);

        // Assert
        Assert.That(result, Is.False);
    }
}