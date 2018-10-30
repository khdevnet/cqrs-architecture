using System;
using System.Collections.Generic;

namespace SW.Checkout.Client.Models
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public ICollection<OrderLineResponseModel> Lines { get; set; } = new List<OrderLineResponseModel>();
    }
}
