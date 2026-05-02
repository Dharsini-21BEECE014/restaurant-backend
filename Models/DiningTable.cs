using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class DiningTable
    {
        [Key] // 👈 FORCE EF to recognize primary key
        public int TableId { get; set; }

        [Required]
        [MaxLength(10)]
        public string TableNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        public TableStatus Status { get; set; } = TableStatus.Available;

        public ICollection<Booking>? Bookings { get; set; }
    }
}