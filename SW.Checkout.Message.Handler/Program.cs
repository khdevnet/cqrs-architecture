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
            IQueueSubscriber subscriber = null;
            IQueueSubscriber readStorageSubscriber = null;
            int attempts_count = 0;
            while (attempts_count <= 20)
            {
                try
                {
                    IContainer container = CreateContainer();

                    subscriber = container.Resolve<IProcessOrderQueueSubscriber>();
                    subscriber.Subscribe();

                    readStorageSubscriber = container.Resolve<IReadStorageSyncQueueSubscriber>();
                    readStorageSubscriber.Subscribe();
                    attempts_count = 21;
                    Console.WriteLine($"### Subscription to Rabbit MQ successful");
                }
                catch (Exception ex)
                {
                    attempts_count += 1;
                    Console.WriteLine($"### Retry connect to Rabbit MQ attempt {attempts_count}");
                }
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
            Console.ReadLine();
            subscriber?.Dispose();
            readStorageSubscriber?.Dispose();
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

            builder.RegisterInstance(CreateConfiguration()).As<IConfiguration>();

            return builder.Build();
        }

        private static IConfiguration CreateConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables("SW_");

            IConfigurationRoot configuration = configBuilder.Build();
            return configuration;
        }
    }
}
