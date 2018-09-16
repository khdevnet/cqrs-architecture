namespace CQRS.Socks.Order.WebApi.Models
{
    public class OrderLineModel
    {
        public class Line
        {
            public int ProductNumber { get; set; }

            public int Quantity { get; set; }
        }
    }
}
