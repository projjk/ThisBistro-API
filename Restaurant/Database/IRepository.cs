using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;

namespace Restaurant.Database;

public interface IRepository
{
    void Add<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    Task<bool> SaveChangesAsync();
    Task<ActionResult<IEnumerable<Menu>>> GetAllMenusAsync();
    Task<Menu?> GetMenuAsync(int menuNo);
    Task<Category?> GetCategoryAsync(int categoryNo);

    void ChangeEntrySate(object entity);
    Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync();
    
    Task<Category?> FindCategory(Menu menu);

    Task<ActionResult<IEnumerable<Order>>> GetAllOrdersAsync();
    Task<Order?> GetOrderAsync(int id);
    Task<ActionResult<IEnumerable<CartItem>>> GetAllCartsAsync();
    Task<CartItem?> GetCartItemAsync(int id);

}