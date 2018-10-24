using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler :
        IMessageHandler<CreateOrder>,
        IMessageHandler<AddOrderLine>,
        IMessageHandler<RemoveOrderLine>
    {
        private readonly IAggregationRepository repository;


        public OrderCommandHandler(
            IAggregationRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateOrder command)
        {
            // TODO: Add customer
            //if (!session.Query<ClientView>().Any(c => c.Id == command.ClientId))
            //    throw new ArgumentException("Client does not exist!", nameof(command.ClientId));

            var order = new OrderAggregate(command.OrderId, command.CustomerId, command.Lines);

            repository.Store(order);
        }

        public void Handle(AddOrderLine message)
        {
            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(message.OrderId);
            orderAggregate.AddLine(message.ProductNumber, message.Quantity);
            repository.Store(orderAggregate);
        }

        public void Handle(RemoveOrderLine message)
        {
            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(message.OrderId);
            orderAggregate.RemoveLine(message.ProductNumber);
            repository.Store(orderAggregate);
        }
    }
}
