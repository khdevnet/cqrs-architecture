using SW.Store.Checkout.Extensibility;
using System;

namespace SW.Store.Checkout.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Guid OrderId { get; set; }

        public OrderLineStatus LineStatus { get; set; } = OrderLineStatus.InStock;

        public Order Order { get; set; }

        public int? WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
