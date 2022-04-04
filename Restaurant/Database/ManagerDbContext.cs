using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Database;

public class ManagerDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public ManagerDbContext(DbContextOptions<ManagerDbContext> options)
        : base(options)
    {
    }
}
