using System;
using Autofac;
using Marten;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Domain.Orders;
using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Infrastructure.EventStore.ViewProjections;
using SW.Store.Checkout.Read.Extensibility;

namespace SW.Store.Checkout.Infrastructure.EventStore
{
    public class EventStoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AggregateRepository>().As<IAggregationRepository>();
            builder.RegisterType<OrderReadRepository>().As<IOrderReadRepository>();
            builder.Register<IDocumentStore>(c =>
             {
                 var documentStore = DocumentStore.For(options =>
                 {
                     var schemaName = "public";

                     options.Connection("PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'swstore'; PASSWORD = '123456'; USER ID = 'postgres'");
                     options.AutoCreateSchemaObjects = AutoCreate.All;
                     options.Events.DatabaseSchemaName = schemaName;
                     options.DatabaseSchemaName = schemaName;

                     options.Events.InlineProjections.AggregateStreamsWith<OrderAggregate>();
                     options.Events.InlineProjections.Add(new OrderViewProjection());
                     //options.Events.InlineProjections.Add(new AccountSummaryViewProjection());
                     //options.Events.InlineProjections.Add(new ClientsViewProjection());

                     options.Events.AddEventType(typeof(OrderCreated));
                 });

                 return documentStore;
             });
        }
    }
}
