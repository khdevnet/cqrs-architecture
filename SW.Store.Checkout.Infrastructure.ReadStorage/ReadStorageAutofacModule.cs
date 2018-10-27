using Autofac;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Infrastructure.ReadStorage.Repositories;
using SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization;
using SW.Store.Checkout.Read.Extensibility;
using SW.Store.Core;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.Infrastructure.ReadStorage
{
    public class ReadStorageAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SwStoreReadDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<OrderReadRepository>().As<IOrderReadRepository>();


            builder.RegisterType<DatabaseInitializer>().As<IInitializer>();
            builder.RegisterType<OrderSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
            builder.RegisterType<WarehouseSyncMessageHandler>().As<IReadStorageSyncMessageHandler>();
        }
    }
}
