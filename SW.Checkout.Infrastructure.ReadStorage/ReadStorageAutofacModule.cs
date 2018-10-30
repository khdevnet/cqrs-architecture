using Autofac;
using Microsoft.EntityFrameworkCore;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Infrastructure.ReadStorage.Repositories;
using SW.Checkout.Infrastructure.ReadStorage.Synchronization;
using SW.Checkout.Read.Extensibility;
using SW.Checkout.Core.Initializers;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Infrastructure.ReadStorage
{
    public class ReadStorageAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //  builder.Register<IDesignTimeDbContextFactory<SwStoreReadDbContext>>(c => new DesignTimeDbContextFactory(c.Resolve<IReadStorageConnectionStringProvider>()));
            //.As<IDesignTimeDbContextFactory<SwStoreReadDbContext>>().PropertiesAutowired();
            builder.Register<SwStoreReadDbContext>(c =>
            {
                var connectionStringProvider = c.Resolve<IReadStorageConnectionStringProvider>();
                var b = new DbContextOptionsBuilder<SwStoreReadDbContext>();
                b.UseNpgsql(connectionStringProvider.Get());
                return new SwStoreReadDbContext(b.Options);

            }).InstancePerLifetimeScope();
            builder.RegisterType<OrderReadRepository>().As<IOrderReadRepository>();

            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
            builder.RegisterType<OrderSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
            builder.RegisterType<WarehouseSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
        }
    }
}
