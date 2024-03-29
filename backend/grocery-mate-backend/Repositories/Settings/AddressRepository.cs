using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using grocery_mate_backend.Service;
using grocery_mate_backend.Utility.Log;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Settings;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private readonly GroceryContext _context;

    public AddressRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Address?> FindAddressByGuid(Guid? guid)
    {
        return await _context.Address
            .Where(a => a.AddressId == guid)
            .FirstOrDefaultAsync();
    }

    public async Task<Address?> FindOrCreateUserAddress(AddressDto addressDto)
    {
        var address = await _context.Address
            .Where(a =>
                a.Street == addressDto.Street &&
                a.HouseNr == addressDto.HouseNr &&
                a.City == addressDto.City &&
                a.ZipCode == addressDto.ZipCode &&
                a.State == addressDto.State)
            .FirstOrDefaultAsync();

        if (address == null || address.AddressId == Guid.Empty)
        {
            var newAddress = _context.Add(new Address(addressDto));
            address = await Task.FromResult(newAddress.Entity)!;
        }

        if (address is { Latitude: 0, Longitude: 0 })
        {
            try
            {
                var coordinates = await GeoApifyApi.GetCoordinates(
                    addressDto.Street,
                    addressDto.HouseNr,
                    addressDto.City,
                    addressDto.ZipCode,
                    addressDto.State);

                address.Longitude = coordinates.lon;
                address.Latitude = coordinates.lat;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return address;
    }

    public Task<bool?> RemoveAddress(Address address, User user)
    {
        try
        {
            address.Users.Remove(user);
            user.AddressId = Guid.Empty;
            if (address.Users.Count == 0)
            {
                _context.Remove(address);
            }

            _context.SaveChanges();
            return Task.FromResult<bool?>(true);
        }
        catch (Exception e)
        {
            GmLogger.Instance.Warn("SettingsRepository: ", "No User found!");
            return Task.FromResult<bool?>(false);
        }
    }
}