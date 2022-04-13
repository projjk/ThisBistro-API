using System.ComponentModel.DataAnnotations;

namespace Restaurant.ViewModels.Manager;

public class RestaurantConfigViewModel
{
    public string OpenHour { get; set; } = null!;
    public string CloseHour { get; set; } = null!;
    [Required]
    public bool? IsOpen { get; set; }
}