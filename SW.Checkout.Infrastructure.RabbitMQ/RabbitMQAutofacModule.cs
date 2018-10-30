using Autofac;
using SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder;
using SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ProcessOrder;
using SW.Store.Core.Queues.ReadStorageSync;

namespace SW.Store.Checkout.Infrastructure.RabbitMQ
{
    public class RabbitMQAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageSender>().As<IMessageSender>();
            builder.RegisterType<ProcessOrderQueueCommandBus>().As<IProcessOrderQueueCommandBus>();
            builder.RegisterType<ProcessOrderQueueSubscriber>().As<IProcessOrderQueueSubscriber>();

            builder.RegisterType<ReadStorageSyncEventBus>().As<IReadStorageSyncEventBus>();
            builder.RegisterType<ReadStorageSyncQueueSubscriber>().As<IReadStorageSyncQueueSubscriber>();
            builder.RegisterType<ReadStorageSyncMessageProcessor>().As<IReadStorageSyncMessageProcessor>();
        }
    }
}
