using System;
using Marten;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Infrastructure.EventStore
{
    internal sealed class AggregateRepository : IAggregationRepository
    {
        private readonly IDocumentStore store;

        public AggregateRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public void Store(EventSourcedAggregate aggregate)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
                Core.Events.IEvent[] events = aggregate.PendingEvents.ToArray();
                session.Events.Append(aggregate.Id, aggregate.PendingEvents.ToArray());
                session.SaveChanges();
            }
            // Once succesfully persisted, clear events from list of uncommitted events
            aggregate.PendingEvents.Clear();
        }

        public T Load<T>(Guid id, int version = 0) where T : class, IAggregate, new()
        {
            using (IDocumentSession session = store.LightweightSession())
            {
                return session.Events.AggregateStream<T>(id, version);
            }

            throw new InvalidOperationException($"No aggregate by id {id}.");
        }
    }
}
