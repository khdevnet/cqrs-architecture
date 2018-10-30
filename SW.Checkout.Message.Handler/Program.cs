using Autofac;
using Microsoft.Extensions.Configuration;
using SW.Checkout.Core;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Domain;
using SW.Checkout.Infrastructure.EventStore;
using SW.Checkout.Infrastructure.RabbitMQ;
using SW.Checkout.Infrastructure.ReadStorage;
using System;
using System.IO;
using System.Threading;

namespace SW.Checkout.Message.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(45));
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

            builder.RegisterInstance(CreateConfiguration());

            return builder.Build();
        }

        private static IConfiguration CreateConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = configBuilder.Build();
            return configuration;
        }
    }
}
