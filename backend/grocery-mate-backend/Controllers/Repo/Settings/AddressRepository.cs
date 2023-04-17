using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using grocery_mate_backend.Services;
using grocery_mate_backend.Services.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Settings;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private readonly GroceryContext _context;

    public AddressRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }

    public Task<Address?> FindAddressByGuid(Guid? guid)
    {
        return _context.Address
            .Where(a => a.AddressId == guid)
            .FirstOrDefaultAsync();
    }

    public Task<Address?> FindOrCreateUserAddress(AddressDto addressDto)
    {
        var address = _context.Address
            .Where(a =>
                a.Street == addressDto.Street &&
                a.HouseNr == addressDto.HouseNr &&
                a.City == addressDto.City &&
                a.ZipCode == addressDto.ZipCode &&
                a.State == addressDto.State)
            .FirstOrDefaultAsync(new CancellationToken());

        if (address == null || address.Result.AddressId == Guid.Empty)
        {
            var newAddress = _context.Add(new Address(addressDto));
            address = Task.FromResult(newAddress.Entity)!;
        }

        return address;
    }

    public Task<bool?> RemoveAddress(Address address, User user)
    {
        try
        {
            address.Users.Remove(user);
            user.AddressId = Guid.Empty;
            _context.Remove(address);
            _context.SaveChanges();
            return Task.FromResult<bool?>(true);
        }
        catch (Exception e)
        {
            GmLogger.GetInstance()?.Warn("SettingsRepository: ", "No User found!");
            return Task.FromResult<bool?>(false);
        }
    }
}