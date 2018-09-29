namespace CQRS.Socks.Order.Domain
{
    public enum OrderStatus
    {
        Created = 0,
        Shipped,
        Delivered,
        Declined
    }
}
