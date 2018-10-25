using System;
using Csrh.TimeReporting.Infrastructure.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Core;

namespace SW.Store.Checkout.Infrastructure.ReadStorage
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly IServiceProvider serviceProvider;

        public int Order { get; } = 1;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Init()
        {
            using (IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                SwStoreReadDbContext context = serviceScope.ServiceProvider.GetRequiredService<SwStoreReadDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }
    }
}
