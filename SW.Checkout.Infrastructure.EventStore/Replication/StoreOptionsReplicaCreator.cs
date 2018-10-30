using SW.Store.Core.Replication;

namespace SW.Store.Checkout.Infrastructure.EventStore.Replication
{
    internal class StoreOptionsReplicaCreator : StoreOptionsCreator
    {
        public StoreOptionsReplicaCreator(IEventStoreReplicaConnectionStringProvider eventStoreConnectionStringProvider) : base(eventStoreConnectionStringProvider)
        {
        }
    }
}
