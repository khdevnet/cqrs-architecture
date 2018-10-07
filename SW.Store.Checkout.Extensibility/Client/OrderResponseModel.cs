using System;
using System.Collections.Generic;
using SW.Store.Checkout.Extensibility.Client;

namespace SW.Store.Checkout.Client.Extensibility.Client
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
