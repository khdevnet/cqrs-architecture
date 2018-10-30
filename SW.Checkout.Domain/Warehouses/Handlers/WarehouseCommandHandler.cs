using SW.Checkout.Domain.Warehouses;
using SW.Checkout.Domain.Warehouses.Commands;
using SW.Checkout.Core.Aggregates;
using SW.Checkout.Core.Messages;

namespace SW.Checkout.Domain.Orders.Handlers
{
    public class WarehouseCommandHandler :
        IMessageHandler<AddWarehouse>
    {
        private readonly IAggregationRepository repository;


        public WarehouseCommandHandler(
            IAggregationRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(AddWarehouse command)
        {
            // TODO: Add customer
            //if (!session.Query<ClientView>().Any(c => c.Id == command.ClientId))
            //    throw new ArgumentException("Client does not exist!", nameof(command.ClientId));

            repository.Store(new WarehouseAggregate(command.Id, command.Name, command.Items));
        }
    }
}
