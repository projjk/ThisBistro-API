using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Database;

public class Repository : IRepository
{
    private readonly ManagerDbContext _context;
    private readonly ILogger<Repository> _logger;

    public Repository(ManagerDbContext context, ILogger<Repository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public void Add<T>(T entity) where T : class 
    {
        _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
        _context.Add(entity);
    }

    public void Remove<T>(T entity) where T: class
    {
        _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
        _context.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        _logger.LogInformation("Attempting to save the changes in the context");

        // Only return success if at least one row was changed
        return (await _context.SaveChangesAsync()) > 0;
    }

    public async Task<User?> GetUserAsync(Guid guid)
    {
        return await _context.Users
            .Include(u => u.CartItems).ThenInclude(c => c.Menu)
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == guid);
    }

    public async Task<IEnumerable<Menu>> GetAllMenusAsync()
    {
        _logger.LogInformation("Getting all Menus");

        return await _context.Menus
            .Include(m => m.Category)
            .ToArrayAsync();
    }

    public async Task<Menu?> GetMenuAsync(int menuNo)
    {
        _logger.LogInformation($"Getting a Menu #{menuNo}");
        return await _context.Menus
            .Include(m => m.Category)
            .FirstOrDefaultAsync(m => m.Id == menuNo);
    }

    public async Task<Category?> GetCategoryAsync(int categoryNo)
    {
        _logger.LogInformation($"Getting a Category #{categoryNo}");
        return await _context.Categories
            .Include(c => c.Menus)
            .FirstOrDefaultAsync(c => c.Id == categoryNo);
    }

    public void ChangeEntrySate(object entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        _logger.LogInformation("Getting all Categories");

        return await _context.Categories.ToArrayAsync();

    }

    public Task<Category?> FindCategory(Menu menu)
    {
        _logger.LogInformation($"Finding a category that has Menu {menu.Name}");
        var category = _context.Categories.FirstOrDefaultAsync(c => c.Menus.Contains(menu));
        return category;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        _logger.LogInformation("Getting all Orders");

        return await _context.Orders.Include(o => o.OrderItems).ToArrayAsync();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        _logger.LogInformation($"Getting an Order #{id}");
        return await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Menu).FirstOrDefaultAsync(o => o.Id == id);
    }
    public async Task<IEnumerable<CartItem>> GetAllCartsAsync()
    {
        _logger.LogInformation("Getting all Carts");

        return await _context.CartItems.Include(c => c.Menu).ToArrayAsync();
    }

    public async Task<CartItem?> GetCartItemAsync(int id)
    {
        _logger.LogInformation($"Getting a CartItem #{id}");
        return await _context.CartItems
            .Include(c => c.Menu)
            .Include(c => c.User)
            .FirstAsync(c => c.Id == id);
    }
    
}