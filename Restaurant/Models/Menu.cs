using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public class Menu
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Sold { get; set; }
    public int Arrangement { get; set; }
    public bool HasPhoto { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public int? CategoryId { get; set; } // Foreign key
    public Category? Category { get; set; } // Inverse navigation property
}