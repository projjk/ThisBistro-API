using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int MenuId { get; set; } // Foreign key
    public Menu Menu { get; set; } = null!; // Inverse navigation property
    public int OrderId { get; set; } // Foreign key
    public Order Order { get; set; } = null!; // Inverse navigation property
}