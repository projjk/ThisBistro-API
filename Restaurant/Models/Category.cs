using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restaurant.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public int Arrangement { get; set; }
    [JsonIgnore]
    public ICollection<Menu> Menus { get; set; } = new List<Menu>();
}