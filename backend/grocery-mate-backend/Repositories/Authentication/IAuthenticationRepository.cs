using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace grocery_mate_backend.Controllers.Repo.Authentication;

public interface IAuthenticationRepository : IGenericRepository<User>
{
    Task<bool> CheckPassword(IdentityUser identityUser, string password);
    Task<IdentityUser> FindIdentityUser(string email);
    Task<IdentityResult?> SaveNewIdentityUser(IdentityUser identityUser, User user);
    AuthenticationResponseDto CreateToken(IdentityUser user, Guid userId);
    AppSettingsGMailDto GetMailSettings();
}