using System.Collections.Generic;

namespace CQRS.Socks.Order.Domain
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
