using SW.Checkout.Core.Replication;

namespace SW.Checkout.Infrastructure.EventStore.Replication
{
    internal class StoreOptionsReplicaCreator : StoreOptionsCreator
    {
        public StoreOptionsReplicaCreator(IEventStoreReplicaConnectionStringProvider eventStoreConnectionStringProvider) : base(eventStoreConnectionStringProvider)
        {
        }
    }
}
