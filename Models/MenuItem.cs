using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public int MenuCategoryId { get; set; }
        public MenuCategory MenuCategory { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}