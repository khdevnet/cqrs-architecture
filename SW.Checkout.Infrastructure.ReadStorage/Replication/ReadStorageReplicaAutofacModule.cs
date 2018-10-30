using Autofac;
using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization;
using SW.Store.Core.Initializers;
using SW.Store.Core.Queues.ReadStorageSync;
using SW.Store.Core.Replication;

namespace SW.Store.Checkout.Infrastructure.ReadStorage
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
