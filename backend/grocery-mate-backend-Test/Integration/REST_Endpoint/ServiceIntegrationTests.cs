using grocery_mate_backend.Controllers;
using grocery_mate_backend.Controllers.Services;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend_Test.Unit.Endpoint;

public partial class ServiceIntegrationTests
{
    DbContextOptions<GroceryContext> _options = new DbContextOptionsBuilder<GroceryContext>()
        .UseInMemoryDatabase(databaseName: "GroceryTestDB")
        .Options;

    private GroceryContext _context;

    private AddressService _addressService;
    private UserService _userService;

    private CreateUserDto _userDtoOne;
    private CreateUserDto _userDtoTwo;
    private AddressDto _addressDtoOne;
    private AddressDto _addressDtoTwo;


    [OneTimeSetUp]
    public void Setup()
    {
        _context = new GroceryContext(_options);
        _context.Database.EnsureCreated();

        _addressService = new AddressService(_context);
        _userService = new UserService(_context);

        SeedDatabase();
    }

    [OneTimeTearDown]
    public void CleanUp()
    {
        _context.Database.EnsureDeleted();
    }

    private void SeedDatabase()
    {
        _userDtoOne = new CreateUserDto(
            "Hans",
            "Mustermann",
            "hans.mustermann@gmail.com",
            "Zürich",
            "test123ABC!");
        var userOne = new User(_userDtoOne);

        _userDtoTwo = new CreateUserDto(
            "Anna",
            "Bernasconi",
            "a.b@outlook.com",
            Symbols.Empty,
            "test123ABC!");
        var userTwo = new User(_userDtoTwo);

        _addressDtoOne = new AddressDto(
            "Hauptstrasse",
            "17A",
            9000,
            "St.Gallen",
            "SG"
        );
        var addressOne = new Address(_addressDtoOne);

        _addressDtoTwo = new AddressDto(
            "Spiecherstrasse",
            "365",
            9500,
            "Wil",
            "SG"
        );
        var addressTwo = new Address(_addressDtoTwo);

        _context.AddRange(userOne, userTwo, addressOne, addressTwo);
        _context.SaveChanges();

        addressTwo.Users.Add(userTwo);
        userTwo.AddressId = addressTwo.AddressId;
        _context.SaveChanges();

        Console.Write("sadf");
    }
    
}