using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required, MaxLength(20)]
        public string OrderNumber { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int TableId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; } = 0;

        public decimal? PaidAmount { get; set; }
        public DateTime? PaidDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}