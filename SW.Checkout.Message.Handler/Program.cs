using System;
using Autofac;
using SW.Checkout.Domain;
using SW.Checkout.Infrastructure.EventStore;
using SW.Checkout.Infrastructure.RabbitMQ;
using SW.Checkout.Infrastructure.ReadStorage;
using SW.Checkout.Core;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Settings;

namespace SW.Checkout.Message.Handler
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

            builder.RegisterModule<DomainAutofacModule>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<CoreAutofacModule>();
            builder.RegisterModule<ReadStorageAutofacModule>();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            builder.RegisterType<ProcessOrderQueueSettingsProvider>().As<IProcessOrderQueueSettingsProvider>();
            builder.RegisterType<ReadStorageSyncQueueSettingsProvider>().As<IReadStorageSyncQueueSettingsProvider>();

            builder.RegisterType<ReadStorageConnectionStringProvider>().As<IReadStorageConnectionStringProvider>();
            builder.RegisterType<EventStoreConnectionStringProvider>().As<IEventStoreConnectionStringProvider>();

            return builder.Build();
        }
    }
}
