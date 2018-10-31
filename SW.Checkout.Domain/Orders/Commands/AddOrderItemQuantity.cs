using System;
using SW.Checkout.Core.Commands;

namespace SW.Checkout.Domain.Orders.Commands
{
    public class AddOrderItemQuantity : ICommand
    {
        public Guid OrderId { get; set; }

        public int ProductNumber { get; set; }

        public int Quantity { get; set; }
    }
}
