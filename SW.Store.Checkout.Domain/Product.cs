using System.Collections.Generic;

namespace SW.Store.Checkout.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<WarehouseItem> WarehouseItems { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
