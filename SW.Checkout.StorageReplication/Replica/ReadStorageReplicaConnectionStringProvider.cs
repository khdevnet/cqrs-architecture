using SW.Checkout.Core.Replication;

namespace SW.Checkout.StorageReplication.Replica
{
    internal class ReadStorageReplicaConnectionStringProvider : IReadStorageReplicaConnectionStringProvider
    {
        public string Get()
        {
            return "Server=127.0.0.1;Port=5432;Database=swstore-rep;User Id=postgres;Password=123456;";
        }
    }
}
