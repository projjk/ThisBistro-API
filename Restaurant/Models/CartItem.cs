namespace Restaurant.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Menu Menu { get; set; }
}