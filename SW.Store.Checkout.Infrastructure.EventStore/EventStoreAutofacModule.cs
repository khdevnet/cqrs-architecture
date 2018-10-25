using Autofac;
using Marten;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Domain.Orders;
using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Domain.Warehouses;
using SW.Store.Checkout.Domain.Warehouses.Events;
using SW.Store.Checkout.Infrastructure.EventStore.Repositories;
using SW.Store.Checkout.Infrastructure.EventStore.ViewProjections.Orders;
using SW.Store.Checkout.Infrastructure.EventStore.ViewProjections.Warehouses;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Core;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.Infrastructure.EventStore
{
    public class EventStoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AggregateRepository>().As<IAggregationRepository>();
            builder.RegisterType<OrderReadRepository>().As<IOrderReadRepository>();
            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
            builder.Register<IDocumentStore>(c =>
             {
                 var documentStore = DocumentStore.For(options =>
                 {
                     string schemaName = "public";
                     string connectionString = c.Resolve<IConnectionStringProvider>().Get();
                     options.Connection(connectionString);
                     options.AutoCreateSchemaObjects = AutoCreate.All;
                     options.Events.DatabaseSchemaName = schemaName;
                     options.DatabaseSchemaName = schemaName;

                     options.Events.InlineProjections.AggregateStreamsWith<OrderAggregate>();
                     options.Events.InlineProjections.Add(new OrderViewProjection());

                     options.Events.AddEventType(typeof(OrderCreated));
                     options.Events.AddEventType(typeof(OrderLineAdded));
                     options.Events.AddEventType(typeof(OrderLineRemoved));

                     options.Events.InlineProjections.AggregateStreamsWith<WarehouseAggregate>();
                     options.Events.InlineProjections.Add(new WarehouseViewProjection());

                     options.Events.AddEventType(typeof(WarehouseCreated));
                     options.Events.AddEventType(typeof(WarehouseItemAdded));
                     options.Events.AddEventType(typeof(WarehouseItemQuantitySubstracted));


                 });

                 return documentStore;
             });
        }
    }
}
