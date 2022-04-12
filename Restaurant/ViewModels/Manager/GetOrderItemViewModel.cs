using Restaurant.Models;

namespace Restaurant.ViewModels.Manager;

public class GetOrderItemViewModel
{
    public int MenuId { get; set; }
    public decimal MenuPrice { get; set; }
    public bool MenuHasPhoto { get; set; }
    public int Quantity { get; set; }
}