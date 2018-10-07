using System;
using System.Collections.Generic;

namespace SW.Store.Checkout.Extensibility.Client
{
    public class CreateOrderRequestModel
    {
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public IEnumerable<CreateOrderLineRequestModel> Lines { get; set; }
    }
}
