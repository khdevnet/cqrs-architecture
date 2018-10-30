using Autofac;
using SW.Checkout.Infrastructure.RabbitMQ.Queues.ProcessOrder;
using SW.Checkout.Infrastructure.RabbitMQ.Queues.ReadStorage;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Queues.ReadStorageSync;

namespace SW.Checkout.Infrastructure.RabbitMQ
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
