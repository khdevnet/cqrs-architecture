using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Core.Initializers;

namespace SW.Checkout.Infrastructure.ReadStorage
{
    internal class DatabaseReplicaInitializer : DatabaseInitializer, IReplicaInitializer
    {
        public DatabaseReplicaInitializer(SwStoreReadDbContext db)
            : base(db)
        {
        }

    }
}
