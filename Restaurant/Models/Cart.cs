namespace Restaurant.Models;

public class Cart
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Menu Menu { get; set; }
}