using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Orders.Views;
using SW.Store.Checkout.Domain.Warehouses.Views;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.EventStore.Replication;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Infrastructure.ReadStorage;
using SW.Store.Checkout.Read.ReadView;
using SW.Store.Checkout.StorageReplication.Replica;
using SW.Store.Checkout.StorageReplication.Replication;
using SW.Store.Core;
using SW.Store.Core.Aggregates;
using SW.Store.Core.Initializers;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ProcessOrder;
using SW.Store.Core.Queues.ReadStorageSync;
using SW.Store.Core.Replication;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.StorageReplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime? timestamp = null;
            if (args.Length > 0)
            {
                timestamp = DateTime.Parse(args[0]).ToUniversalTime();
                Console.WriteLine($"### Replication start for period before {args[0]}");
            }
            IContainer container = CreateContainer();

            IEnumerable<IInitializer> initializers = container.Resolve<IEnumerable<IReplicaInitializer>>();
            RunInitializers(initializers);

            IEnumerable<IReplicationManager> replicationManagers = container.Resolve<IEnumerable<IReplicationManager>>();
            RunReplicators(replicationManagers, timestamp);

            IQueueSubscriber readStorageSubscriber = container.Resolve<IReadStorageSyncQueueSubscriber>();
            readStorageSubscriber.Subscribe();

            Console.ReadLine();

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

            builder.RegisterType<ProcessOrderQueueSettingsProvider>().As<IProcessOrderQueueSettingsProvider>();
            builder.RegisterType<ReadStorageSyncQueueSettingsProvider>().As<IReadStorageSyncQueueSettingsProvider>();

            builder.RegisterType<ReadStorageConnectionStringProvider>().As<IReadStorageConnectionStringProvider>();
            builder.RegisterType<EventStoreConnectionStringProvider>().As<IEventStoreConnectionStringProvider>();

            builder.RegisterType<ReadStorageReplicaConnectionStringProvider>().As<IReadStorageReplicaConnectionStringProvider>();
            builder.RegisterType<EventStoreReplicaConnectionStringProvider>().As<IEventStoreReplicaConnectionStringProvider>();

            return builder.Build();
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
