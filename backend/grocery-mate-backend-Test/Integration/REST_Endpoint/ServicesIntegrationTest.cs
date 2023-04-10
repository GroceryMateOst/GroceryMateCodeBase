using grocery_mate_backend.Controllers;
using grocery_mate_backend.Controllers.Services;
using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using grocery_mate_backend.Sandbox;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using User = grocery_mate_backend.Data.DataModels.UserManagement.User;

namespace grocery_mate_backend_Test.Unit.Endpoint;

public class ServicesTest
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

    [Test, Order(1)]
    public void FindUserTest_BasedOnEmail_true()
    {
        var result = _userService.FindUser("hans.mustermann@gmail.com");
        Assert.Multiple(() =>
        {
            Assert.That(result.Result?.FirstName, Is.EqualTo("Hans"));
            Assert.That(result.Result?.SecondName, Is.EqualTo("Mustermann"));
        });
    }

    [Test, Order(2)]
    public void FindOrCreateAddressTest_FindAddress_true()
    {
        var result = _addressService.FindOrCreateAddress(_addressDtoOne);
        Assert.Multiple(() =>
        {
            Assert.That(result.Result?.Street, Is.EqualTo(_addressDtoOne.Street));
            Assert.That(result.Result?.HouseNr, Is.EqualTo(_addressDtoOne.HouseNr));
            Assert.That(result.Result?.City, Is.EqualTo(_addressDtoOne.City));
            Assert.That(result.Result?.ZipCode, Is.EqualTo(_addressDtoOne.ZipCode));
            Assert.That(result.Result?.State, Is.EqualTo(_addressDtoOne.State));
        });
    }

    [Test, Order(3)]
    public void FindOrCreateAddressTest_CreateNewAddress_true()
    {
        var newAddress = new AddressDto(
            "Oberdorfstrasse",
            "1",
            8000,
            "Frauenfeld",
            "TG"
        );

        var result = _addressService.FindOrCreateAddress(newAddress);
        Assert.Multiple(() =>
        {
            Assert.That(result.Result?.Street, Is.EqualTo(newAddress.Street));
            Assert.That(result.Result?.HouseNr, Is.EqualTo(newAddress.HouseNr));
            Assert.That(result.Result?.City, Is.EqualTo(newAddress.City));
            Assert.That(result.Result?.ZipCode, Is.EqualTo(newAddress.ZipCode));
            Assert.That(result.Result?.State, Is.EqualTo(newAddress.State));
        });
    }

    [Test, Order(4)]
    public void FindAddressByGuidTest_true()
    {
        var newAddressDto = new AddressDto(
            "Eschenstrasse",
            "18",
            6000,
            "Chur",
            "GR"
        );
        var newAddress = new Address(newAddressDto);

        _context.AddRange(newAddress);
        _context.SaveChanges();

        var reverenceResult = _addressService.FindOrCreateAddress(newAddressDto);
        var result = _addressService.FindAddressByGuid(reverenceResult.Result?.AddressId);

        Assert.That(result.Result?.AddressId, Is.EqualTo(reverenceResult.Result!.AddressId));
    }

    [Test, Order(4)]
    public void RemoveAddressTest_true()
    {
        var newUserDto = new CreateUserDto(
            "Hans",
            "Mustermann",
            "hans.mustermann@gmail.com",
            "Zürich",
            "test123ABC!");
        var newUser = new User(newUserDto);

        var newAddressDto = new AddressDto(
            "Buchenstrasse",
            "98",
            6000,
            "Chur",
            "GR"
        );
        var newAddress = new Address(newAddressDto);

        _context.AddRange(newAddress, newUser);
        _context.SaveChanges();

        var reverenceResult = _addressService.FindOrCreateAddress(newAddressDto);
        Assert.Multiple(() =>
        {
            Assert.That(reverenceResult.Result?.Street, Is.EqualTo(newAddressDto.Street));
            Assert.That(reverenceResult.Result?.HouseNr, Is.EqualTo(newAddressDto.HouseNr));
        });
        _addressService.RemoveAddress(newAddress, newUser);

        var checkResult = _addressService.FindOrCreateAddress(newAddressDto);
        Assert.That(checkResult.Result?.AddressId, !Is.EqualTo(reverenceResult.Result?.AddressId));
    }
}