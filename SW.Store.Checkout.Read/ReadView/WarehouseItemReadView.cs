using System;

namespace SW.Store.Checkout.Read.ReadView
{
    public class WarehouseItemReadView
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Guid WarehouseId { get; set; }

        public WarehouseReadView Warehouse { get; set; }
    }
}
