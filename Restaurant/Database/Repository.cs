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

    public async Task<ActionResult<IEnumerable<Menu>>> GetAllMenusAsync()
    {
        _logger.LogInformation("Getting all Menus");

        return await _context.Menus.ToArrayAsync();
    }

    public async Task<Menu?> GetMenuAsync(int menuNo)
    {
        _logger.LogInformation($"Getting a Menu #{menuNo}");
        return await _context.Menus.FindAsync(menuNo);
    }

    public async Task<Category?> GetCategoryAsync(int categoryNo)
    {
        _logger.LogInformation($"Getting a Category #{categoryNo}");
        return await _context.Categories.FindAsync(categoryNo);
    }

    public void ChangeEntrySate(object entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
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
}