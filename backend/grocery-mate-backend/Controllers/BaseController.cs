using grocery_mate_backend.Data;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using grocery_mate_backend.Services;
using Microsoft.EntityFrameworkCore;
using User = grocery_mate_backend.Models.User;

namespace grocery_mate_backend.Controllers;

[ApiController]
[Route("api/v0/base")]
// Base class from which all further REST API controllers derive
public class BaseController : ControllerBase
{
    public GroceryContext _context;
    public UserManager<IdentityUser> _userManager;
    public JwtService _jwtService;

    public BaseController(GroceryContext context, UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _context = context;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    protected async Task<User?> FindUser(string email)
    {
        return await _context.User
            .Where(u => u.EmailAddress == email)
            .FirstOrDefaultAsync();
    }

    protected async Task<IdentityUser> FindIdentityUser(string email)
    {
        return await _userManager.FindByNameAsync(email);
    }

    protected async Task<Address?> FindAddressByGuid(Guid? addressId)
    {
        return await _context.Address
            .Where(a => a.AddressId == addressId)
            .FirstOrDefaultAsync();
    }

    protected async Task<Address?> FindAddress(AddressDto address)
    {
        var user = await _context.Address
            .Where(a =>
                a.Street == address.Street &&
                a.HouseNr == address.HouseNr &&
                a.City == address.City &&
                a.ZipCode == address.ZipCode)
            .FirstOrDefaultAsync();
        return user;
    }

    protected async Task<bool> CheckPassword(IdentityUser identityUser, string password)
    {
        return await _userManager.CheckPasswordAsync(identityUser, password);
    }
}