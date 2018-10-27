using SW.Store.Checkout.Infrastructure.EventStore.Repositories;
using SW.Store.Core.Replication;

namespace SW.Store.Checkout.Infrastructure.EventStore.Replication
{
    internal class AggregateReplicaRepository : AggregateRepository, IAggregationReplicaRepository
    {
        public AggregateReplicaRepository(IDocumentStoreReplica store) : base(store)
        {
        }
    }
}
