using Marten;
using SW.Checkout.Core.Settings;
using SW.Checkout.Domain.Orders.Events;
using SW.Checkout.Domain.Warehouses.Events;
using SW.Checkout.Infrastructure.EventStore.ViewProjections.Orders;
using SW.Checkout.Infrastructure.EventStore.ViewProjections.Warehouses;

namespace SW.Checkout.Infrastructure.EventStore
{
    internal class StoreOptionsCreator
    {
        private readonly IEventStoreConnectionStringProvider eventStoreConnectionStringProvider;

        public StoreOptionsCreator(IEventStoreConnectionStringProvider eventStoreConnectionStringProvider)
        {
            this.eventStoreConnectionStringProvider = eventStoreConnectionStringProvider;
        }

        public virtual StoreOptions Create()
        {
            var options = new StoreOptions();
            string schemaName = "public";
            string connectionString = eventStoreConnectionStringProvider.Get();
            options.Connection(connectionString);
            options.AutoCreateSchemaObjects = AutoCreate.All;
            options.Events.DatabaseSchemaName = schemaName;
            options.DatabaseSchemaName = schemaName;

            // options.Events.InlineProjections.AggregateStreamsWith<OrderAggregate>();
            options.Events.InlineProjections.Add(new OrderViewProjection());

            options.Events.AddEventType(typeof(OrderCreated));
            options.Events.AddEventType(typeof(OrderLineAdded));
            options.Events.AddEventType(typeof(OrderLineRemoved));

            //  options.Events.InlineProjections.AggregateStreamsWith<WarehouseAggregate>();
            options.Events.InlineProjections.Add(new WarehouseViewProjection());

            options.Events.AddEventType(typeof(WarehouseCreated));
            options.Events.AddEventType(typeof(WarehouseItemAdded));
            options.Events.AddEventType(typeof(WarehouseItemQuantitySubstracted));
            return options;

        }
    }
}
