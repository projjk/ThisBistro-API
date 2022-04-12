using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public ICollection<Menu> Menus { get; set; } = new List<Menu>();
}