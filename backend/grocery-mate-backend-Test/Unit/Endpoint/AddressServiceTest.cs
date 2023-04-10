using System.Net;
using Castle.Components.DictionaryAdapter.Xml;
using grocery_mate_backend.Controllers.EndpointControllers;
using grocery_mate_backend.Controllers.Services;
using grocery_mate_backend.Data;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using grocery_mate_backend.Models.Settings;
using grocery_mate_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using NUnit.Framework;

namespace grocery_mate_backend_Test.Unit.Endpoint;

public class AddressServiceTest
{
    private AddressService _addressService;

    private readonly Mock<GroceryContext> _contextMock = new Mock<GroceryContext>();

    public AddressServiceTest()
    {
        _addressService = new AddressService(_contextMock.Object);
    }

    [Test]
    public Task FindOrCreateAddressTest_FindAddress_true()
    {
        var newAddressDto = new AddressDto(
            "Hauptstrasse",
            "17A",
            9000,
            "St.Gallen",
            "SG"
        );
        var addressDto = new Address(
            Guid.NewGuid(),
            "Hauptstrasse",
            "17A",
            9000,
            "St.Gallen",
            "SG",
            new Coordinate(),
            new List<User>()
        );

        var result = _addressService.FindOrCreateAddress(newAddressDto);

        var type = _contextMock.Setup(x => x.Add(new Address(newAddressDto)))
            .GetType();
        return Task.CompletedTask;
        //
        //     _contextMock.Setup(x =>
        //             x.Address.Where(
        //                     a =>
        //                         a.Street == newAddressDto.Street &&
        //                         a.HouseNr == newAddressDto.HouseNr &&
        //                         a.City == newAddressDto.City &&
        //                         a.ZipCode == newAddressDto.ZipCode &&
        //                         a.State == newAddressDto.State)
        //                 .FirstOrDefaultAsync(new CancellationToken()))
        //         .ReturnsAsync(addressDto);
        //
        //     Assert.That(result.Result?.Street, Is.EqualTo(newAddressDto.Street));
        //     Assert.That(result.Result?.HouseNr, Is.EqualTo(newAddressDto.HouseNr));
        //     Assert.That(result.Result?.City, Is.EqualTo(newAddressDto.City));
        //     Assert.That(result.Result?.ZipCode, Is.EqualTo(newAddressDto.ZipCode));
        //     Assert.That(result.Result?.State, Is.EqualTo(newAddressDto.State));
    }
}