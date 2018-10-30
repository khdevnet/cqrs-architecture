using Autofac;
using SW.Store.Core.Replication;

namespace SW.Store.Checkout.Infrastructure.EventStore.Replication
{
    public class EventStoreReplicaAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AggregateReplicaRepository>().As<IAggregationReplicaRepository>();
            builder.Register<IDocumentStoreReplica>(c => new DocumentStoreReplica(new StoreOptionsCreator(c.Resolve<IEventStoreReplicaConnectionStringProvider>()).Create()));
        }
    }
}
