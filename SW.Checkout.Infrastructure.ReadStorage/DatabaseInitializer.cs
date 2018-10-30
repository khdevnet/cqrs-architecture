using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Core.Initializers;

namespace SW.Store.Checkout.Infrastructure.ReadStorage
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly SwStoreReadDbContext context;

        public int Order { get; } = 1;

        public DatabaseInitializer(SwStoreReadDbContext context)
        {
            this.context = context;
        }

        public void Init()
        {
            MigrateDatabase(context);
        }

        private static void MigrateDatabase(SwStoreReadDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
