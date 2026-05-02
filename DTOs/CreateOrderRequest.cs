namespace RestaurantAPI.DTOs
{
    public class CreateOrderRequest
    {
        public int BookingId { get; set; }
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderItemRequest
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}