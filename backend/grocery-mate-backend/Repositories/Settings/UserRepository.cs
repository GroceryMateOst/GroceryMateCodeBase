using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Utility.Log;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Controllers.Repo.Settings;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly GroceryContext _context;

    public UserRepository(GroceryContext context) : base(context)
    {
        _context = context;
    }

    public Task<User?> FindUserByMail(string email)
    {
        return _context.User
                .Where(u => u.EmailAddress == email)
                .FirstOrDefaultAsync();
    }

    public Task<User?> FindUserByIdentityId(string id)
    {
        return _context.User
                .Where(u => u.Identity.Id == id)
                .Include(user => user.Address)
                .FirstOrDefaultAsync();
    }
    
    public Task<User?> FindUserById(Guid id)
    {
        return _context.User
            .Where(u => u.UserId == id)
            .Include(user => user.Address)
            .FirstOrDefaultAsync();
    }
}