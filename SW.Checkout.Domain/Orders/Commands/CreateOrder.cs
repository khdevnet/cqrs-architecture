using System;
using System.Collections.Generic;
using SW.Checkout.Domain.Orders.Dto;
using SW.Checkout.Core.Commands;

namespace SW.Checkout.Domain.Orders.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid OrderId { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
