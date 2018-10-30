using SW.Checkout.Core.Replication;

namespace SW.Checkout.StorageReplication.Replica
{
    internal class EventStoreReplicaConnectionStringProvider : IEventStoreReplicaConnectionStringProvider
    {
        public string Get()
        {
            return "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'swstore-rep'; PASSWORD = '123456'; USER ID = 'postgres'";
        }
    }
}
