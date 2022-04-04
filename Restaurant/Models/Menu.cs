namespace Restaurant.Models;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Sold { get; set; }
    public int Arrangement { get; set; }
    public bool HasPhoto { get; set; }
}