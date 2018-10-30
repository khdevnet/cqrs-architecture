using SW.Store.Checkout.Domain.Orders.Dto;
using System.Collections.Generic;

namespace SW.Store.Checkout.WebApi.Models
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }

        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
