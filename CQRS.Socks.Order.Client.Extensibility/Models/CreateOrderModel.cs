using System;
using System.Collections.Generic;

namespace CQRS.Socks.Order.Client.Extensibility.Models
{
    public class CreateOrderModel
    {
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public IEnumerable<OrderLineModel> Lines { get; set; }
    }
}
