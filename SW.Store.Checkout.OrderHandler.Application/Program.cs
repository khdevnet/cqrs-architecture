using System;
using Autofac;
using SW.Store.Checkout.Domain.Orders.Handlers;
using SW.Store.Checkout.Extensibility.Queues.ProcessOrder;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Core;
using SW.Store.Core.Messages;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.OrderHandler.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = CreateContainer();

            IQueueSubscriber subscriber = container.Resolve<IProcessOrderQueueSubscriber>();
            subscriber.Subscribe();

            IQueueSubscriber readStorageSubscriber = container.Resolve<IReadStorageSyncQueueSubscriber>();
            readStorageSubscriber.Subscribe();


            Console.ReadLine();
            subscriber.Dispose();
            readStorageSubscriber.Dispose();
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(OrderCommandHandler)).As<IMessageHandler>();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<CoreAutofacModule>();
            builder.RegisterModule<ReadStorageAutofacModule>();

            builder.RegisterType(typeof(ProcessOrderQueueSettingsProvider)).As<IProcessOrderQueueSettingsProvider>();
            builder.RegisterType(typeof(ReadStorageSyncQueueSettingsProvider)).As<IReadStorageSyncQueueSettingsProvider>();

            builder.RegisterType<ReadStorageConnectionStringProvider>().As<IReadStorageConnectionStringProvider>();
            builder.RegisterType<EventStoreConnectionStringProvider>().As<IEventStoreConnectionStringProvider>();

            return builder.Build();
        }
    }
}
