namespace RestaurantAPI.Models
{

    public enum BookingStatus
    {
        Confirmed,
        Seated,
        Completed,
        Cancelled
    }

    public enum OrderStatus
    {
        Pending = 0,
        Preparing = 1,
        Served = 2,
        Completed = 3,
        Billed = 4
    }

    public enum KitchenStatus
    {
        Pending,
        Preparing,
        Ready,
        Served
    }

}