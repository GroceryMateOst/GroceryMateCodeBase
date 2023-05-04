using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.UOW;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Service;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace grocery_mate_backend_Test.Unit.Service;

using System.Threading.Tasks;
using NUnit.Framework;

public class UserServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task GetAuthenticatedUser_WithValidIdentityName_ReturnsUser()
    {
        // Arrange
        const string identityName = "testuser";
        var identityUser = new IdentityUser { Id = "12345" };
        var user = new User { UserId = Guid.NewGuid() };
        _unitOfWorkMock.Setup(uow => uow.Authentication.FindIdentityUser(identityName))
            .ReturnsAsync(identityUser);
        _unitOfWorkMock.Setup(uow => uow.User.FindUserByIdentityId(identityUser.Id))
            .ReturnsAsync(user);

        // Act
        var result = await UserService.GetAuthenticatedUser(identityName, _unitOfWorkMock.Object);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(user.UserId));
    }

    [Test]
    public async Task GetAuthenticatedUser_WithNullIdentityName_ReturnsNull()
    {
        // Act
        var result = await UserService.GetAuthenticatedUser(null, _unitOfWorkMock.Object);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAuthenticatedUser_WithNullUser_ReturnsNull()
    {
        // Arrange
        const string identityName = "testuser";
        var identityUser = new IdentityUser { Id = "12345" };
        _unitOfWorkMock.Setup(uow => uow.Authentication.FindIdentityUser(identityName))
            .ReturnsAsync(identityUser);
        _unitOfWorkMock.Setup(uow => uow.User.FindUserByIdentityId(identityUser.Id))
            .ReturnsAsync((User)null);

        // Act
        var result = await UserService.GetAuthenticatedUser(identityName, _unitOfWorkMock.Object);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAuthenticatedUser_WhenIdentityNameIsNull_ReturnsNull()
    {
        // Arrange
        string identityName = null;
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        // Act
        var result = await UserService.GetAuthenticatedUser(identityName, mockUnitOfWork.Object);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateAddress_WithValidAddress_ReturnsTrue()
    {
        // Arrange
        var address = new Address { AddressId = Guid.NewGuid() };

        // Act
        var result = ValidationBase.ValidateAddress(address, "methodName");

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ValidateAddress_WithNullAddress_ReturnsFalse()
    {
        // Arrange
        Address address = null;

        // Act
        var result = ValidationBase.ValidateAddress(address, "methodName");

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidateAddress_WithEmptyAddressId_ReturnsFalse()
    {
        // Arrange
        var address = new Address { AddressId = Guid.Empty };

        // Act
        var result = ValidationBase.ValidateAddress(address, "methodName");

        // Assert
        Assert.That(result, Is.False);
    }
}