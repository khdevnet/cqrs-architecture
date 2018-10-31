using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using SW.Checkout.Core;
using SW.Checkout.Core.Aggregates;
using SW.Checkout.Core.Initializers;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Core.Replication;
using SW.Checkout.Domain;
using SW.Checkout.Domain.Orders.Views;
using SW.Checkout.Domain.Warehouses.Views;
using SW.Checkout.Infrastructure.EventStore;
using SW.Checkout.Infrastructure.EventStore.Replication;
using SW.Checkout.Infrastructure.RabbitMQ;
using SW.Checkout.Infrastructure.ReadStorage;
using SW.Checkout.Read.ReadView;
using SW.Checkout.StorageReplication.Replica;
using SW.Checkout.StorageReplication.Replication;

namespace SW.Checkout.StorageReplication
{
    class Program
    {
        static void Main(string[] args)
        {

            IContainer container = CreateContainer();

            string timestampConfig = container.Resolve<IConfiguration>().GetSection("EventStoreReplica")["timestamp"];

            DateTime? timestamp = null;
            if (!string.IsNullOrEmpty(timestampConfig))
            {
                timestamp = DateTime.Parse(timestampConfig).ToUniversalTime();
                Console.WriteLine($"### Replication start for period before {timestampConfig}");
            }

            IEnumerable<IInitializer> initializers = container.Resolve<IEnumerable<IReplicaInitializer>>();
            RunInitializers(initializers);

            IEnumerable<IReplicationManager> replicationManagers = container.Resolve<IEnumerable<IReplicationManager>>();
            RunReplicators(replicationManagers, timestamp);

            IQueueSubscriber readStorageSubscriber = container.Resolve<IReadStorageSyncQueueSubscriber>();
            readStorageSubscriber.Subscribe();

            Console.ReadLine();
            readStorageSubscriber.Dispose();
        }

        private static IEnumerable<OrderReadView> GetOrder(IAggregationRepository originalAggregationRepostory)
        {
            return originalAggregationRepostory.Query<OrderView, OrderReadView>((w) => new OrderReadView { Id = w.Id });
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<DomainAutofacModule>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<EventStoreReplicaAutofacModule>();

            builder.RegisterModule<CoreAutofacModule>();
            builder.RegisterModule<ReadStorageReplicaAutofacModule>();

            builder.RegisterType<ReplicationManager<OrderView>>().As<IReplicationManager>();
            builder.RegisterType<ReplicationManager<WarehouseView>>().As<IReplicationManager>();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            builder.RegisterType<ReadStorageReplicaConnectionStringProvider>().As<IReadStorageReplicaConnectionStringProvider>();
            builder.RegisterType<EventStoreReplicaConnectionStringProvider>().As<IEventStoreReplicaConnectionStringProvider>();

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


        public static void RunReplicators(IEnumerable<IReplicationManager> replicators, DateTime? timestamp)
        {
            if (replicators != null)
            {
                replicators
                    .ToList()
                    .ForEach(rep => rep.Replicate(timestamp));
            }
        }

        public static void RunInitializers(IEnumerable<IInitializer> initializers)
        {

            if (initializers != null)
            {
                initializers
                    .OrderBy(i => i.Order)
                    .ToList()
                    .ForEach(initializer => initializer.Init());
            }
        }
    }
}
