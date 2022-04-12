namespace Restaurant.ViewModels;

public class GetMenuViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool HasPhoto { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}