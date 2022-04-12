using Restaurant.Models;

namespace Restaurant.ViewModels.Manager;

public class GetOrderViewModel
{
    public int Id { get; set; }
    public Order.OrderStatusEnum Status { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public string? Memo { get; set; }
    public ICollection<GetOrderItemViewModel> OrderItems { get; set; } = new List<GetOrderItemViewModel>();
    public Guid UserId { get; set; } // Foreign key
}