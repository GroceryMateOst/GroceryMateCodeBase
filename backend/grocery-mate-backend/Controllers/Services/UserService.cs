using grocery_mate_backend.Data;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using Microsoft.EntityFrameworkCore;


namespace grocery_mate_backend.Controllers.Services;

public class UserService
{
    private GroceryContext _context;

    public UserService(GroceryContext context)
    {
        _context = context;
    }

    public Task<User?> FindUser(string email)
    {
        return _context.User
            .Where(u => u.EmailAddress == email)
            .FirstOrDefaultAsync();
    }
}