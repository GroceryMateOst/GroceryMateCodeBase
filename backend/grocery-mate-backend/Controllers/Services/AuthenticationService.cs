using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Controllers.Services;

public class AuthenticationService
{
    private UserManager<IdentityUser> _userManager;

    public AuthenticationService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser> FindIdentityUser(string email)
    {
        return await _userManager.FindByNameAsync(email);
    }

    public async Task<bool> CheckPassword(IdentityUser identityUser, string password)
    {
        return await _userManager.CheckPasswordAsync(identityUser, password);
    }

    public Task<IdentityResult?> SaveNewIdentityUser(IdentityUser identityUser, User user)
    {
        return _userManager.CreateAsync(
            identityUser,
            user.Password
        );
        
    }
}