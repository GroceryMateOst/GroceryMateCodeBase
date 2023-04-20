using grocery_mate_backend.Controllers.Repo.Settings;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Data.DataModels.UserManagement.Address;
using grocery_mate_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Integration.Repo.Settings;

public class AddressRepositoryTest
{
    private AddressRepository _addressRepository;
    private GroceryContext _context;
    private IConfiguration _configuration;


    private DbContextOptions<GroceryContext> _options = new DbContextOptionsBuilder<GroceryContext>()
        .UseInMemoryDatabase(databaseName: "GroceryTestDB")
        .Options;

    [SetUp]
    public void SetUp()
    {
        _context = new GroceryContext(_options);
        _context.Database.EnsureCreated();
        _addressRepository = new AddressRepository(_context, _configuration);
    }

    [Test]
    public void AddAddress()
    {
        var addressDto = new AddressDto(
            "Pizzastrasse",
            "45",
            8052,
            "Oerlikon",
            "Zürich");
        
        var address1 = new Address(
            new AddressDto("Pizzastrasse",
                "42",
                8052,
                "Oerlikon",
                "Zürich")
        );
        
        var address2 = new Address(
            addressDto
        );

        _context.Add(address1);
        _context.Add(address2);
        _context.SaveChanges();
        
        var addressByGuid = _addressRepository.FindAddressByGuid(address1.AddressId);
        Assert.That(addressByGuid.Result?.AddressId, Is.EqualTo(address1.AddressId));
        
        var findAddressByGuid = _addressRepository.FindOrCreateUserAddress(addressDto).Result;
        Assert.That(findAddressByGuid?.AddressId, Is.EqualTo(address2.AddressId));
    }
    
    [Test]
    public void RemoveAddress()
    {
        var hansMuster = new User(
            new CreateUserDto(
                "Hans",
                "Mustermann",
                "hans.mustermann@gmail.com",
                "Zürich",
                "test123ABC!")
        );
        
        var address = new Address(
            new AddressDto("Pizzastrasse",
                "42",
                8052,
                "Oerlikon",
                "Zürich")
        );
        
        _context.Add(address);
        var client = _context.Add(hansMuster);
        client.Entity.AddressId = address.AddressId;
        _context.SaveChanges();

        _addressRepository.RemoveAddress(address, client.Entity);
        var addressByGuid = _addressRepository.FindAddressByGuid(address.AddressId);
        
        Assert.Multiple(() =>
        {
            Assert.That(client.Entity.AddressId, Is.EqualTo(Guid.Empty));
            Assert.That(addressByGuid.Result, Is.Null);
        });
    }
}