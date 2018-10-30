using System;
using System.Collections.Generic;

namespace SW.Checkout.Read.ReadView
{
    public class WarehouseReadView
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<WarehouseItemReadView> Items { get; set; } = new List<WarehouseItemReadView>();
    }
}
