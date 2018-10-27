using SW.Store.Checkout.Domain.Orders.Enum;

namespace SW.Store.Checkout.Domain.Orders
{
    public class OrderLine
    {
        public string Status { get; set; } = OrderLineStatus.InStock.ToString();

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
