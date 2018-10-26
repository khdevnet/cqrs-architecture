using Autofac;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Checkout.Infrastructure.ReadStorage;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization;
using SW.Store.Core;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class ReadStorageAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SwStoreReadDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
            builder.RegisterType<OrderSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
            builder.RegisterType<WarehouseSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
        }
    }
}
