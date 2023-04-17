using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.Endpoint;

public partial class ServiceIntegrationTests
{
    [Test]
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

    [Test]
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

    [Test]
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

    [Test]
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