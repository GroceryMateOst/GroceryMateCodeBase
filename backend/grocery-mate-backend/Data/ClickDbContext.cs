using Microsoft.EntityFrameworkCore;
using grocery_mate_backend.Models;

namespace grocery_mate_backend.Data;

public class ClickDbContext : DbContext
{
    public ClickDbContext(DbContextOptions<ClickDbContext> options) : base(options)
    {

    }

    public DbSet<Click> Clicks {get; set;}
}