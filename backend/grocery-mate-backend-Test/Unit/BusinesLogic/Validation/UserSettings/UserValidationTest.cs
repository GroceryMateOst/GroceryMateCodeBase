using grocery_mate_backend.BusinessLogic.Validation.UserSettings;
using grocery_mate_backend.Utility.Log;
using Moq;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation.UserSettings;

public class UserValidationTests
{
    [Test]
    public void ValidateUser_ValidUser_ReturnsTrue()
    {
        // Arrange
        var user = new object();
        var methodName = "TestMethod";

        // Act
        var result = UserValidation.ValidateUser(user, methodName);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateUser_NullUser_ReturnsFalse()
    {
        // Arrange
        object? user = null;
        var methodName = "TestMethod";
        var loggerMock = new Mock<GmLogger>();

        // Act
        var result = UserValidation.ValidateUser(user, methodName);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateUserMail_ValidMailAddress_ReturnsTrue()
    {
        // Arrange
        var mailAddress = "test@example.com";
        var methodName = "TestMethod";

        // Act
        var result = UserValidation.ValidateUserMail(mailAddress, methodName);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateUserMail_EmptyMailAddress_ReturnsFalse()
    {
        // Arrange
        var mailAddress = string.Empty;
        var methodName = "TestMethod";
        var loggerMock = new Mock<GmLogger>();

        // Act
        var result = UserValidation.ValidateUserMail(mailAddress, methodName);

        // Assert
        Assert.That(result, Is.False);
    }
}
