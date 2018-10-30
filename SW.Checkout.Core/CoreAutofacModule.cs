using Autofac;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MessageProcessor)).As<IMessageProcessor>();
            builder.RegisterType<MessageDeserializer>().As<IMessageDeserializer>();

            builder.RegisterType<EventStoreConnectionStringProvider>().As<IEventStoreConnectionStringProvider>();
            builder.RegisterType<ReadStorageConnectionStringProvider>().As<IReadStorageConnectionStringProvider>();
            builder.RegisterType<ProcessOrderQueueSettingsProvider>().As<IProcessOrderQueueSettingsProvider>();
            builder.RegisterType<ReadStorageSyncQueueSettingsProvider>().As<IReadStorageSyncQueueSettingsProvider>();
        }
    }
}
