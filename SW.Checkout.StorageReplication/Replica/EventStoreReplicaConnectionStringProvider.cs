using Microsoft.Extensions.Configuration;
using SW.Checkout.Core.Replication;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.StorageReplication.Replica
{
    internal class EventStoreReplicaConnectionStringProvider : ConnectionStringProviderBase, IEventStoreReplicaConnectionStringProvider
    {
        public EventStoreReplicaConnectionStringProvider(IConfiguration configuration) : base(configuration, "EventStoreReplica")
        {

        }
    }
}
