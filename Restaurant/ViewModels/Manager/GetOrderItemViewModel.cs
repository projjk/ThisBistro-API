using Restaurant.Models;

namespace Restaurant.ViewModels.Manager;

public class GetOrderItemViewModel
{
    public string MenuName { get; set; } = null!;
    public decimal MenuPrice { get; set; }
    public bool MenuHasPhoto { get; set; }
    public int Quantity { get; set; }
}