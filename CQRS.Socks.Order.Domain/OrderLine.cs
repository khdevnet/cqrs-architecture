using System;

namespace CQRS.Socks.Order.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
