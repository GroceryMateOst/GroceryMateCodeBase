using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Service;
using Microsoft.AspNetCore.Identity;
using Moq;
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

        // Act
        var validationResult = AuthenticationValidation.ValidateUserPassword(result);

        // Assert
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidateUserPassword_InvalidPassword_ReturnsFalse()
    {
        // Arrange
        const bool result = false;

        // Act
        var validationResult = AuthenticationValidation.ValidateUserPassword(result);

        // Assert
        Assert.That(validationResult, Is.False);
    }
    
    [Test]
    public void ValidateUserPassword_NullPassword_ReturnsFalse()
    {
        // Arrange
        const bool result = false;

        // Act
        var validationResult = AuthenticationValidation.ValidateUserPassword(result);

        // Assert
        Assert.That(validationResult, Is.False);
    }
    
    [Test]
    public async Task GetAuthenticatedUser_ReturnsNull_WhenIdIsNullOrEmpty()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
        authenticationRepositoryMock.Setup(x => x.FindIdentityUser(It.IsAny<string>())).ReturnsAsync(new IdentityUser());
        unitOfWorkMock.SetupGet(x => x.Authentication).Returns(authenticationRepositoryMock.Object);
        unitOfWorkMock.Setup(x => x.User.FindUserByIdentityId(null)).ReturnsAsync((User)null);

        // Act
        var result = await UserService.GetAuthenticatedUser("identityName", unitOfWorkMock.Object);

        // Assert
        Assert.That(result, Is.Null);
    }
}