using Restaurant.Models;

namespace Restaurant.ViewModels;

public class GetOrderViewModel
{
    public int Id { get; set; }
    public Order.OrderStatusEnum Status { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public string? Memo { get; set; }
    public ICollection<Manager.GetOrderItemViewModel> OrderItems { get; set; } = new List<Manager.GetOrderItemViewModel>();
}