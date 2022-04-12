using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Database;

public class ManagerDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    private DbSet<RestaurantConfig> RestaurantConfigs { get; set; } = null!;

    public ManagerDbContext(DbContextOptions<ManagerDbContext> options)
        : base(options)
    {
    }
    
    // Using DbSet<RestaurantConfig> RestaurantConfigs as if it's a static property.
    public RestaurantConfig RestaurantConfig
    {
        get
        {
            var tmp = RestaurantConfigs.FirstOrDefault();
            if (tmp == null)
            {
                tmp = new RestaurantConfig();
                RestaurantConfigs.Add(tmp);
            }

            return tmp;
        }
    }
}