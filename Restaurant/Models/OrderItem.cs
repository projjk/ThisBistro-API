namespace Restaurant.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Menu Menu { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}