using SW.Checkout.Domain.Orders.Dto;
using System.Collections.Generic;

namespace SW.Checkout.WebApi.Models
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
