using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using System;

namespace SW.Store.Checkout.Infrastructure.SQL
{
    public static class DatabaseExtensions
    {
        public static void RegisterDbContext(this IServiceCollection collection, string connection)
        {
            collection.AddDbContext<SwStoreDbContext>
                (options => options.UseSqlServer(connection));
        }

        public static void MigrateDbContext(this IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                SwStoreDbContext context = serviceScope.ServiceProvider.GetRequiredService<SwStoreDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }
    }
}
