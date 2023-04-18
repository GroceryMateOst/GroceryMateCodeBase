using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.Endpoint;

[TestFixture]
public class AddressServiceTests
{
    // private GroceryContext _context;
    // private AddressService _service;
    //
    //
    // private Address addressOne;
    // private Address addressTwo;
    //
    // [SetUp]
    // public void SetUp()
    // {
    //     var options = new DbContextOptionsBuilder<GroceryContext>()
    //         .UseInMemoryDatabase(databaseName: "GroceryTestDatabase")
    //         .Options;
    //
    //     _context = new GroceryContext(options);
    //
    //     var addressDtoOne = new AddressDto
    //     {
    //         Street = "123 Main St",
    //         HouseNr = "Apt 4",
    //         City = "Anytown",
    //         ZipCode = 12345,
    //         State = "CA"
    //     };
    //
    //     var addressDtoTwo = new AddressDto
    //     {
    //         Street = "123 Main St",
    //         HouseNr = "Apt 4",
    //         City = "Anytown",
    //         ZipCode = 12345,
    //         State = "CA"
    //     };
    //
    //     addressOne = new Address(addressDtoOne);
    //     addressTwo = new Address(addressDtoTwo);
    //
    //     var user = new User
    //     {
    //         FirstName = "John",
    //         SecondName = "Doe",
    //         EmailAddress = "john@example.com"
    //     };
    //     addressOne.Users.Add(user);
    //
    //     _context.Address.Add(addressOne);
    //     _context.Address.Add(addressTwo);
    //     _context.User.Add(user);
    //     _context.SaveChanges();
    //
    //     _service = new AddressService(_context);
    // }
    //
    // [TearDown]
    // public void TearDown()
    // {
    //     _context.Database.EnsureDeleted();
    //     _context.Dispose();
    // }
    //
    // [Test]
    // public async Task FindAddressByGuid_ShouldReturnAddress()
    // {
    //     // Arrange
    //     var address = _context.Address.First();
    //
    //     // Act
    //     var result = await _service.FindAddressByGuid(address.AddressId);
    //
    //     // Assert
    //     Assert.That(result.Equals(address));
    // }
    //
    // [Test]
    // public void RemoveAddress_ShouldRemoveAddressFromContext()
    // {
    //     // Arrange
    //     var address = _context.Address.First();
    //     var user = _context.User.First();
    //
    //     // Act
    //     _service.RemoveAddress(address, user);
    //     Assert.Multiple(() =>
    //     {
    //         // Assert
    //         Assert.That(_context.Address.Contains(addressOne), Is.False);
    //         Assert.That(_context.User.First().AddressId, Is.EqualTo(Guid.Empty));
    //     });
    // }
}