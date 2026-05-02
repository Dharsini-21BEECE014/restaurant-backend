using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required, MaxLength(20)]
        public string BookingNumber { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(15)]
        public string CustomerPhone { get; set; } = string.Empty;

        public int TableId { get; set; }

        public DiningTable? Table { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public int GuestCount { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}