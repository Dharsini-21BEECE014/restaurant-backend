using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class MenuCategory
    {
        public int MenuCategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}