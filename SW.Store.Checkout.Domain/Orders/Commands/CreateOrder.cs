using System;
using System.Collections.Generic;
using SW.Store.Checkout.Domain.Orders.Dto;
using SW.Store.Core.Commands;

namespace SW.Store.Checkout.Domain.Orders.Commands
{
    public class CreateOrder : ICommand
    {
        public Guid OrderId { get; set; }

        public int CustomerId { get; set; }

        public IEnumerable<OrderLineDto> Lines { get; set; }
    }
}
