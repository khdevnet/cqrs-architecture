using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Extensibility.Client
{
    public class OrderResponseModel
    {
        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public Guid OrderId { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderLineResponseModel> Lines { get; set; } = new List<OrderLineResponseModel>();
    }
}
