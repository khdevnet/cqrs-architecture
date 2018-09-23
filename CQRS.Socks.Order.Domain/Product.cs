using System;
using System.Collections.Generic;

namespace CQRS.Socks.Order.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Item Item { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
