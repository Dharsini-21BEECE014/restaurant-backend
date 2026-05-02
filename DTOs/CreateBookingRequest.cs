namespace RestaurantAPI.DTOs
{
    public class CreateBookingRequest
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int TableId { get; set; }
        public int GuestCount { get; set; }
        public DateTime BookingDate { get; set; }
    }
}