using System.Collections.Generic;

namespace SW.Store.Checkout.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShippingAddress { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
