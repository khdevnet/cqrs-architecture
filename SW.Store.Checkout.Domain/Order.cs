using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = OrderStatus.Created.ToString();

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<OrderLine> Lines { get; set; }
    }
}
