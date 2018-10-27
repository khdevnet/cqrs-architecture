using Autofac;
using Marten;
using SW.Store.Checkout.Infrastructure.EventStore.Repositories;
using SW.Store.Core.Aggregates;
using SW.Store.Core.Initializers;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.Infrastructure.EventStore
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
