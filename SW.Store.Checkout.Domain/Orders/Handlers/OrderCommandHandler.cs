using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Core.Commands;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler
        : ICommandHandler<CreateOrder>
    {
        private readonly IAggregationRepository repository;
        private readonly IEventBus eventBus;


        public OrderCommandHandler(
            IAggregationRepository repository,
            IEventBus eventBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateOrder command, CancellationToken cancellationToken = default(CancellationToken))
        {
            // TODO: Add customer
            //if (!session.Query<ClientView>().Any(c => c.Id == command.ClientId))
            //    throw new ArgumentException("Client does not exist!", nameof(command.ClientId));

            var order = new OrderAggregate(command.OrderId, command.CustomerId, command.Lines);

            repository.Store(order);
            await eventBus.Publish(order.PendingEvents.ToArray());

            return Unit.Value;
        }
    }
}
