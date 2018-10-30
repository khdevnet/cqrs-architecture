using SW.Store.Core.Replication;

namespace SW.Store.Checkout.StorageReplication.Replica
{
    internal class ReadStorageReplicaConnectionStringProvider : IReadStorageReplicaConnectionStringProvider
    {
        public string Get()
        {
            return "Server=127.0.0.1;Port=5432;Database=swstore-rep;User Id=postgres;Password=123456;";
        }
    }
}
