using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Core.Commands;

namespace SW.Store.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler
        : ICommandHandler<CreateOrder>
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
    }
}
