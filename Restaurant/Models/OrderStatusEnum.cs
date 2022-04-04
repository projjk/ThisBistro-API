namespace Restaurant.Models;

public partial class Order
{
    public enum OrderStatusEnum
    {
        Received,
        Confirmed,
        Ready,
        Paid,
        Cancelled
    }
}