using System;
using SW.Checkout.Domain.Orders.Enum;

namespace SW.Checkout.Domain.Orders
{
    public class OrderLine
    {
        public string Status { get; set; } = OrderLineStatus.InStock.ToString();

        public int ProductId { get; set; }

        public Guid WarehouseId { get; set; }

        public int Quantity { get; set; }
    }
}
