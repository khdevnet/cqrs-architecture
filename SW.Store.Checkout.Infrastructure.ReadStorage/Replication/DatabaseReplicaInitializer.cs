using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Core.Initializers;

namespace SW.Store.Checkout.Infrastructure.ReadStorage
{
    internal class DatabaseReplicaInitializer : DatabaseInitializer, IReplicaInitializer
    {
        public DatabaseReplicaInitializer(SwStoreReadDbContext db)
            : base(db)
        {
        }

    }
}
