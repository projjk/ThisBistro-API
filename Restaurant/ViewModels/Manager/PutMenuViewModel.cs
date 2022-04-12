namespace Restaurant.ViewModels.Manager;

public class PutMenuViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool HasPhoto { get; set; }
    public int CategoryId { get; set; }
}