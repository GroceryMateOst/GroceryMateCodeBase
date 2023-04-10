using grocery_mate_backend.Data;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Services;

public class AddressService
{
    private GroceryContext _context;

    public AddressService(GroceryContext context)
    {
        _context = context;
    }

    public async Task<Address?> FindOrCreateAddress(AddressDto addressDto)
    {
        var address = await _context.Address
            .Where(a =>
                a.Street == addressDto.Street &&
                a.HouseNr == addressDto.HouseNr &&
                a.City == addressDto.City &&
                a.ZipCode == addressDto.ZipCode &&
                a.State == addressDto.State)
            .FirstOrDefaultAsync(new CancellationToken());

        if (address == null || address.AddressId == Guid.Empty)
        {
            address = new Address(addressDto);
            _context.Add(address);
        }

        return address;
    }

    public async Task<Address?> FindAddressByGuid(Guid? addressId)
    {
        return await _context.Address
            .Where(a => a.AddressId == addressId)
            .FirstOrDefaultAsync();
    }

    public void RemoveAddress(Address address, User user)
    {
        address.Users.Remove(user);
        user.AddressId = Guid.Empty;
        _context.Remove(address);
        _context.SaveChanges();
    }
}