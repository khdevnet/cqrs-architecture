namespace CQRS.Socks.Order.Client.Extensibility.Models
{
    public class OrderLineRequestModel
    {
        public int ProductNumber { get; set; }

        public int Quantity { get; set; }
    }
}
