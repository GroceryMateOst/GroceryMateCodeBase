using Microsoft.EntityFrameworkCore;
using grocery_mate_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace grocery_mate_backend.Data;

public class GroceryContext : IdentityUserContext<IdentityUser>
{
    public GroceryContext(DbContextOptions<GroceryContext> options) : base(options)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Address> Address { get; set; }
}