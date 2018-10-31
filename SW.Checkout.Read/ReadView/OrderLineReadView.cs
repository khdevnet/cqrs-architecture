using System;

namespace SW.Checkout.Read.ReadView
{
    public class OrderLineReadView
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Guid WarehouseId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }

        public Guid OrderId { get; set; }

        public OrderReadView Order { get; set; }
    }
}
