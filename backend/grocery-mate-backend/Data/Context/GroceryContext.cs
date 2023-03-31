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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // builder.Entity<User>().OwnsOne(x => x.);
    }

    public DbSet<User> User { get; set; }
    public DbSet<Address> Address { get; set; }
}