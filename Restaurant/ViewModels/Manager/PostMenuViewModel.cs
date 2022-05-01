namespace Restaurant.ViewModels.Manager;

public class PostMenuViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool HasPhoto { get; set; }
    public int Arrangement { get; set; }
    public int CategoryId { get; set; }
}