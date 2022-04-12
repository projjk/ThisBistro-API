using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int MenuId { get; set; } // Foreign key
    public Menu Menu { get; set; } = null!; // Inverse navigation property

    public Guid UserId { get; set; } // Foreign key
    public User User { get; set; } = null!; // Inverse navigation property
}