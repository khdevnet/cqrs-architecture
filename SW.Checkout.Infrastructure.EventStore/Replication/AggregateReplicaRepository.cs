using SW.Checkout.Infrastructure.EventStore.Repositories;
using SW.Checkout.Core.Replication;

namespace SW.Checkout.Infrastructure.EventStore.Replication
{
    internal class AggregateReplicaRepository : AggregateRepository, IAggregationReplicaRepository
    {
        public AggregateReplicaRepository(IDocumentStoreReplica store) : base(store)
        {
        }
    }
}
