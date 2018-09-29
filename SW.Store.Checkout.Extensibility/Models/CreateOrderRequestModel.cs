using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Client.Extensibility.Models
{
    public class CreateOrderRequestModel
    {
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public IEnumerable<OrderLineRequestModel> Lines { get; set; }
    }
}
