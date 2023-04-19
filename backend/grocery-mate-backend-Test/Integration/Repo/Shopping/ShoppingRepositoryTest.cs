using grocery_mate_backend.BusinessLogic.Validation.Shopping;
using grocery_mate_backend.Controllers.Repo.Shopping;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Integration.Repo.Shopping;

public class ShoppingRepositoryTest
{
    private ShoppingRepository _shoppingRepository;
    private GroceryContext _context;

    private DbContextOptions<GroceryContext> _options = new DbContextOptionsBuilder<GroceryContext>()
        .UseInMemoryDatabase(databaseName: "GroceryTestDB")
        .Options;


    [SetUp]
    public void SetUp()
    {
        _context = new GroceryContext(_options);
        _context.Database.EnsureCreated();
        _shoppingRepository = new ShoppingRepository(_context);
    }

    [Test]
    public void FindGroceryRequest_Published_true()
    {
        const string clientMail = "hans.mustermann@gmail.com";
        const string contractorMail = "anna.bernasconi@outlook.com";

        var hansMuster = new User(
            new CreateUserDto(
                "Hans",
                "Mustermann",
                clientMail,
                "Zürich",
                "test123ABC!")
        );

        var annaBernasconi = new User(new CreateUserDto(
            "Anna",
            "Bernasconi",
            contractorMail,
            Symbols.Empty,
            "test123ABC!"));

        var request = new GroceryRequest(
            new ValidatedGroceryRequest(
                hansMuster,
                annaBernasconi,
                new ValidatedShoppingList(),
                RatingState.P));

        _context.AddRange(hansMuster, annaBernasconi);
        _context.GroceryRequests.Add(request);
        _context.SaveChanges();

        var result = _shoppingRepository.FindGroceryRequest(contractorMail, contractorMail);

        // Assert.That(result.Result, Is.EqualTo(request));
    }
}