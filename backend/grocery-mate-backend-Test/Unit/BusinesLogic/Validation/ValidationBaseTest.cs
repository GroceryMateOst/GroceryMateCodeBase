using grocery_mate_backend.BusinessLogic.Validation;
using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.BusinesLogic.Validation;

public class ValidationBaseTests
{
  private Mock<ICanceledTokensRepository> _mockTokenRepository;

    [SetUp]
    public void Setup()
    {
        _mockTokenRepository = new Mock<ICanceledTokensRepository>();
    }

    [Test]
    public void ValidateModel_ReturnsTrue_WhenModelIsValidAndTokenIsValid()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        var headers = new HeaderDictionary();
        headers["Authorization"] = "Bearer validtoken";

        _mockTokenRepository.Setup(x => x.ValidateToken("validtoken")).ReturnsAsync(true);

        // Act
        var result = ValidationBase.ValidateModel(modelState, headers, _mockTokenRepository.Object);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateModel_ReturnsFalse_WhenModelIsInvalid()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("field", "error");

        var headers = new HeaderDictionary();
        headers["Authorization"] = "Bearer validtoken";

        _mockTokenRepository.Setup(x => x.ValidateToken("validtoken")).ReturnsAsync(true);

        // Act
        var result = ValidationBase.ValidateModel(modelState, headers, _mockTokenRepository.Object);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateModel_ReturnsFalse_WhenTokenIsInvalid()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        var headers = new HeaderDictionary();
        headers["Authorization"] = "Bearer invalidtoken";

        _mockTokenRepository.Setup(x => x.ValidateToken("invalidtoken")).ReturnsAsync(false);

        // Act
        var result = ValidationBase.ValidateModel(modelState, headers, _mockTokenRepository.Object);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateSessionToken_ReturnsTrue_WhenTokenIsValid()
    {
        // Arrange
        _mockTokenRepository.Setup(x => x.ValidateToken("validtoken")).ReturnsAsync(true);

        // Act
        var result = ValidationBase.ValidateSessionToken("validtoken", _mockTokenRepository.Object);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void ValidateSessionToken_ReturnsFalse_WhenTokenIsInvalid()
    {
        // Arrange
        _mockTokenRepository.Setup(x => x.ValidateToken("invalidtoken")).ReturnsAsync(false);

        // Act
        var result = ValidationBase.ValidateSessionToken("invalidtoken", _mockTokenRepository.Object);

        // Assert
        Assert.IsFalse(result);
    }
    
    
}