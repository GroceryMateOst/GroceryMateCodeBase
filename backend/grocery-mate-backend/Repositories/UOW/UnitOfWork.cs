using grocery_mate_backend.Controllers.Repo.Authentication;
using grocery_mate_backend.Controllers.Repo.Settings;
using grocery_mate_backend.Controllers.Repo.Shopping;
using grocery_mate_backend.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Controllers.Repo.UOW;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GroceryContext _context;

    public IAuthenticationRepository Authentication { get; }
    public IUserRepository User { get; }
    public IShoppingRepository Shopping { get; }
    public IAddressRepository Address { get; }


    public UnitOfWork(GroceryContext context, IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        _context = context;
        User = new UserRepository(_context);
        Shopping = new ShoppingRepository(_context);
        Address = new AddressRepository(_context, configuration);
        Authentication = new AuthenticationRepository(context, userManager, configuration);
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}