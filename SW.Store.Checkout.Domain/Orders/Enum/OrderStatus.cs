namespace SW.Store.Checkout.Domain.Orders.Enum
{
    public enum OrderStatus
    {
        NotExist = 0,
        Created,
        Shipped,
        Delivered,
        Declined
    }
}
