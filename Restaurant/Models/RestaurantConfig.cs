namespace Restaurant.Models;

public class RestaurantConfig
{
    public int Id { get; set; }
    public TimeOnly OpenHour { get; set; } = TimeOnly.Parse("08:00:00");
    public TimeOnly CloseHour { get; set; } = TimeOnly.Parse("21:00:00");
    public bool IsOpen { get; set; }
}