using System;
using System.Collections.Generic;
using System.Linq;
using SW.Checkout.Core;
using SW.Checkout.Core.Aggregates;
using SW.Checkout.Core.Events;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Core.Replication;
using SW.Checkout.Core.Views;

namespace SW.Checkout.StorageReplication.Replication
{
    internal class ReplicationManager<TView> : IReplicationManager
        where TView : IView
    {
        private readonly IAggregationRepository aggregationRepository;
        private readonly IAggregationReplicaRepository aggregationReplicaRepository;
        private readonly IReadStorageSyncEventBus readStorageSyncEventBus;
        private readonly ILogger logger;

        public ReplicationManager(
            IAggregationRepository aggregationRepository,
            IAggregationReplicaRepository aggregationReplicaRepository,
            IReadStorageSyncEventBus readStorageSyncEventBus,
            ILogger logger)
        {
            this.aggregationRepository = aggregationRepository;
            this.aggregationReplicaRepository = aggregationReplicaRepository;
            this.readStorageSyncEventBus = readStorageSyncEventBus;
            this.logger = logger;
        }

        public void Replicate(DateTime? timestamp = null)
        {
            IEnumerable<Guid> streamIds = aggregationRepository.Query<TView, Guid>((w) => w.Id);

            foreach (Guid streamId in streamIds)
            {
                IEnumerable<IEvent> events = aggregationRepository.GetEvents(streamId, timestamp: timestamp);

                var aggEvents = new Dictionary<Guid, List<IEvent>>();
                aggEvents.Add(streamId, events.ToList());
                Action transactionPostProcessFunc = () => aggEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));
                aggregationReplicaRepository.Transaction(() => aggEvents, transactionPostProcessFunc);
                logger.Log($"### StreamId: {streamId}");
            }
        }
    }
}
