using System;
using System.Collections.Generic;

namespace SW.Checkout.WebApi.Models
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public ICollection<OrderLineReadModel> Lines { get; set; } = new List<OrderLineReadModel>();
    }
}
