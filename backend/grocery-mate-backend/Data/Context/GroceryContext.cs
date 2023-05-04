using grocery_mate_backend.Data.DataModels.Authentication;
using grocery_mate_backend.Data.DataModels.Shopping;
using grocery_mate_backend.Data.DataModels.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace grocery_mate_backend.Data.Context;

public class GroceryContext : IdentityUserContext<IdentityUser>
{
    public GroceryContext(DbContextOptions<GroceryContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Address> Address { get; set; }
    public DbSet<GroceryRequest> GroceryRequests { get; set; }
    public DbSet<TokenBlacklistEntry> CanceledTokens { get; set; }
}