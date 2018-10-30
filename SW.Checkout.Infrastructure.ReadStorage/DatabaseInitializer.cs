using Microsoft.EntityFrameworkCore;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Core.Initializers;

namespace SW.Checkout.Infrastructure.ReadStorage
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
