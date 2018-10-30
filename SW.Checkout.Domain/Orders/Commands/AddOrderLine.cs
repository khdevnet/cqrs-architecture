using System;
using SW.Store.Core.Commands;

namespace SW.Store.Checkout.Domain.Orders.Commands
{
    public class AddOrderLine : ICommand
    {
        public Guid OrderId { get; set; }

        public int ProductNumber { get; set; }

        public int Quantity { get; set; }
    }
}
