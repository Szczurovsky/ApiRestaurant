using System.ComponentModel.DataAnnotations;

namespace ApiRestaurant.Models
{
    public class ModifyRestaurantDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
