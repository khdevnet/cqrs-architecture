using Autofac;
using SW.Store.Checkout.Extensibility.Queues.ProcessOrder;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder;
using SW.Store.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage;
using SW.Store.Core.Messages;

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
