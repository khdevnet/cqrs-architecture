using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Read
{
    public class OrderReadDto
    {
        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public Guid OrderId { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderLineReadDto> Lines { get; set; } = new List<OrderLineReadDto>();
    }
}
