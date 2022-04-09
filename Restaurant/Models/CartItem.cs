using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    [Required]
    public Menu? Menu { get; set; }
}