using System.Collections.Generic;

namespace CQRS.Socks.Order.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShippingAddress { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
