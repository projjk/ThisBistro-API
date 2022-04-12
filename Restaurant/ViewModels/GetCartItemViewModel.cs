using System.ComponentModel.DataAnnotations;
using Restaurant.Models;

namespace Restaurant.ViewModels;

public class GetCartItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int MenuId { get; set; }
    public string MenuName { get; set; } = null!;
    public decimal MenuPrice { get; set; }
    public bool MenuHasPhoto { get; set; }
}