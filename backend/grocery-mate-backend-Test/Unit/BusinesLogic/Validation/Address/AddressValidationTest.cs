using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Data.DataModels.UserManagement;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation;

public class AddressValidationTest
{
    [Test]
    public void ValidateZipCode_ValidZipCode_ReturnsTrue()
    {
        // Arrange
        int zipcode = 8166;

        // Act
        var validationResult = AddressValidation.ValidateZipcode(zipcode);

        // Assert
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidateZipCode_TooSmallZipcode_ReturnsFalse()
    {
        // Arrange
        var zipcode = 0;

        // Act
        var validationResult = AddressValidation.ValidateZipcode(zipcode);

        // Assert
        Assert.That(validationResult, Is.False);
    }
    
    [Test]
    public void ValidateZipCode_TooBigZipcode_ReturnsFalse()
    {
        // Arrange
        var zipcode = 10000;

        // Act
        var validationResult = AddressValidation.ValidateZipcode(zipcode);

        // Assert
        Assert.That(validationResult, Is.False);
    }
    
    [Test]
    public void ValidateAddress_WithValidAddress_ReturnsTrue()
    {
        // Arrange
        var address = new Address { AddressId = Guid.NewGuid() };

        // Act
        var result = AddressValidation.ValidateAddress(address);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateAddress_WithNullAddress_ReturnsFalse()
    {
        // Arrange
        Address address = null;

        // Act
        var result =  AddressValidation.ValidateAddress(address);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateAddress_WithEmptyAddressId_ReturnsFalse()
    {
        // Arrange
        var address = new Address { AddressId = Guid.Empty };

        // Act
        var result =  AddressValidation.ValidateAddress(address);

        // Assert
        Assert.That(result, Is.False);
    }
}