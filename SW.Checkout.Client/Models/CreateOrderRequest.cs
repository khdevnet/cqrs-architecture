using System.Collections.Generic;

namespace SW.Checkout.Client.Models
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }

        public IEnumerable<OrderLineRequestModel> Lines { get; set; }
    }
}
