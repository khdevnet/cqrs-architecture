using System;
using System.Collections.Generic;

namespace SW.Checkout.Read.ReadView
{
    public class OrderReadView
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public ICollection<OrderLineReadView> Lines { get; set; } = new List<OrderLineReadView>();
    }
}
