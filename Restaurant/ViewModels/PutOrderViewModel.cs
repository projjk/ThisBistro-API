using Restaurant.Models;

namespace Restaurant.ViewModels;

public class PutOrderViewModel
{
    public int Id { get; set; }
    public Order.OrderStatusEnum Status { get; set; }
}