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
    public void ValidateZipCode_InvalidZipCode_ReturnsFalse()
    {
        // Arrange
        var zipcode = 0;
        var zipcode1 = 10000;

        // Act
        var validationResult = AddressValidation.ValidateZipcode(zipcode);
        var validationResult1 = AddressValidation.ValidateZipcode(zipcode1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(validationResult, Is.False);
            Assert.That(validationResult1, Is.False);
        });
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