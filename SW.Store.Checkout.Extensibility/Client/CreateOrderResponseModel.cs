using SW.Store.Checkout.Extensibility.Client;
using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Client.Extensibility.Client
{
    public class CreateOrderResponseModel
    {
        public Guid OrderId { get; set; }

        public string Status { get; set; }

        public IEnumerable<OrderLineResponseModel> Lines { get; set; } = new List<OrderLineResponseModel>();
    }
}
