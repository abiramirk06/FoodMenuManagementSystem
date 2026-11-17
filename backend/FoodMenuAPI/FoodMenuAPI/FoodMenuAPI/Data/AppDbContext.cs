

using FoodMenuAPI.Auth;
using FoodMenuAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FoodItem>()
            .Property(f => f.Price)
            .HasPrecision(10, 2);
    }


    public DbSet<FoodItem> FoodItems { get; set; }
    public DbSet<AppUser> Users { get; set; }
}
