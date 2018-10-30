using Autofac;
using Microsoft.EntityFrameworkCore;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Infrastructure.ReadStorage.Synchronization;
using SW.Checkout.Core.Initializers;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Replication;

namespace SW.Checkout.Infrastructure.ReadStorage
{
    public class ReadStorageReplicaAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<SwStoreReadDbContext>(c =>
            {
                IReadStorageReplicaConnectionStringProvider connectionStringProvider = c.Resolve<IReadStorageReplicaConnectionStringProvider>();
                var b = new DbContextOptionsBuilder<SwStoreReadDbContext>();
                b.UseNpgsql(connectionStringProvider.Get());
                return new SwStoreReadDbContext(b.Options);

            }).InstancePerLifetimeScope();
            builder.RegisterType<DatabaseReplicaInitializer>().As<IReplicaInitializer>();
            builder.RegisterType<OrderSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
            builder.RegisterType<WarehouseSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
        }
    }
}
