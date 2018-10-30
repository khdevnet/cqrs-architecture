using Autofac;
using Marten;
using SW.Checkout.Infrastructure.EventStore.Repositories;
using SW.Checkout.Core.Aggregates;
using SW.Checkout.Core.Initializers;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Infrastructure.EventStore
{
    public class EventStoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AggregateRepository>().As<IAggregationRepository>();
            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
            builder.Register<IDocumentStore>(c => new DocumentStore(new StoreOptionsCreator(c.Resolve<IEventStoreConnectionStringProvider>()).Create()));
        }
    }
}
