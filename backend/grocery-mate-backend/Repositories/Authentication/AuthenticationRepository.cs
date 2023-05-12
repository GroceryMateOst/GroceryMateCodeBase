using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using grocery_mate_backend.Controllers.Repo.Generic;
using grocery_mate_backend.Data.Context;
using grocery_mate_backend.Data.DataModels.UserManagement;
using grocery_mate_backend.Models.Authentication;
using grocery_mate_backend.Utility.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace grocery_mate_backend.Controllers.Repo.Authentication;

public class AuthenticationRepository : GenericRepository<User>, IAuthenticationRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    private static int _expirationMinutes = 1;

    public AuthenticationRepository(GroceryContext context, UserManager<IdentityUser> userManager,
        IConfiguration configuration, int expirationMinutes) : base(context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _expirationMinutes = expirationMinutes;
    }

    public override async Task<IEnumerable<User>> All()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception)
        {
            GmLogger.Instance.Warn("AuthenticationRepository: ", "No User found!");
            return new List<User>();
        }
    }

    public async Task<bool> CheckPassword(IdentityUser identityUser, string password)
    {
        try
        {
            return await _userManager.CheckPasswordAsync(identityUser, password);
        }
        catch (Exception)
        {
            GmLogger.Instance.Trace("AuthenticationRepository: ", "Password-Check failed!");
            return false;
        }
    }

    public async Task<IdentityUser> FindIdentityUser(string email)
    {
        return await _userManager.FindByNameAsync(email);
    }

    public Task<IdentityResult?> SaveNewIdentityUser(IdentityUser identityUser, User user)
    {
        Task<IdentityResult?> result = null;
        try
        {
            result = _userManager.CreateAsync(
                identityUser,
                user.Password
            );
        }
        catch (Exception)
        {
            GmLogger.Instance.Trace("AuthenticationRepository: ", "Password-Check failed!");
        }

        return result;
    }

    public AuthenticationResponseDto CreateToken(IdentityUser user, Guid userId)
    {
        var expiration = DateTime.UtcNow.AddMinutes(_expirationMinutes);

        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponseDto
        {
            Token = tokenHandler.WriteToken(token.Result),
            Expiration = expiration,
            Email = user.Email,
            UserId = userId
        };
    }

    public AppSettingsGMailDto GetMailSettings()
    {
        var sectionGMail = _configuration.GetSection("GMail");
        return new AppSettingsGMailDto(
            sectionGMail.GetSection("FromMail").Value, 
            sectionGMail.GetSection("Host").Value, 
            Convert.ToInt32(sectionGMail.GetSection("Port").Value), 
            sectionGMail.GetSection("AppPassword").Value
            );
    }
    
    private Task<JwtSecurityToken>
        CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
        Task.FromResult(new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        ));

    private Claim[] CreateClaims(IdentityUser user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,
                _configuration["Jwt:Subject"] ?? throw new InvalidOperationException()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

    private SigningCredentials CreateSigningCredentials() =>
        new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException())
            ),
            SecurityAlgorithms.HmacSha256
        );
}