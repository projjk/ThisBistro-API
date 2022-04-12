namespace Restaurant.Models;

public partial class Order
{
    public int Id { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public string? Memo { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public Guid UserId { get; set; } // Foreign key
    public User User { get; set; } = null!; // Inverse navigation property
}