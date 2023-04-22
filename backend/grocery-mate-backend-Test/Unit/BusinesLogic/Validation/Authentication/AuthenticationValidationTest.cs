using grocery_mate_backend.BusinessLogic.Validation.Authentication;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation.Authentication;

public class AuthenticationValidationTests
{
    [Test]
    public void ValidateIdentityUserCreation_ValidIdentityResult_ReturnsTrue()
    {
        // Arrange
        var result = IdentityResult.Success;
        const string methodName = "TestMethodName";

        // Act
        var validationResult = AuthenticationValidation.ValidateIdentityUserCreation(result, methodName);

        // Assert
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidateIdentityUserCreation_InvalidIdentityResult_ReturnsFalse()
    {
        // Arrange
        var result = IdentityResult.Failed(new IdentityError {Description = "TestError"});
        const string methodName = "TestMethodName";

        // Act
        var validationResult = AuthenticationValidation.ValidateIdentityUserCreation(result, methodName);

        // Assert
        Assert.That(validationResult, Is.False);
    }

    [Test]
    public void ValidateUserPassword_ValidPassword_ReturnsTrue()
    {
        // Arrange
        const bool result = true;
        const string methodName = "TestMethodName";

        // Act
        var validationResult = AuthenticationValidation.ValidateUserPassword(result, methodName);

        // Assert
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidateUserPassword_InvalidPassword_ReturnsFalse()
    {
        // Arrange
        const bool result = false;
        const string methodName = "TestMethodName";

        // Act
        var validationResult = AuthenticationValidation.ValidateUserPassword(result, methodName);

        // Assert
        Assert.That(validationResult, Is.False);
    }
}