using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Marten;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Core.Aggregates;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Infrastructure.EventStore.Repositories
{
    internal sealed class AggregateRepository : IAggregationRepository
    {
        private readonly IDocumentStore store;

        public AggregateRepository(IDocumentStore store)
        {
            this.store = store;
        }

        public IEnumerable<TResult> Query<TSource, TResult>(Expression<Func<TSource, TResult>> selector)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session
                .Query<TSource>()
                .Select(selector)
                .ToList();
            }
        }

        public TSource FirstOrDefault<TSource>(Expression<Func<TSource, bool>> predicate)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session
                .Query<TSource>()
                .FirstOrDefault(predicate);
            }
        }

        public void Transaction(Func<Dictionary<Guid, List<IEvent>>> transaction, Action postProcess = null)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                IEnumerable<KeyValuePair<Guid, List<IEvent>>> events = transaction();
                events.ToList().ForEach(aggEvents => session.Events.Append(aggEvents.Key, aggEvents.Value.ToArray()));
                session.SaveChanges();

                postProcess?.Invoke();
            }
        }

        public void Store(EventSourcedAggregate aggregate)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
                IEvent[] events = aggregate.PendingEvents.ToArray();
                session.Events.Append(aggregate.Id, aggregate.PendingEvents.ToArray());
                session.SaveChanges();
            }
            // Once succesfully persisted, clear events from list of uncommitted events
            aggregate.PendingEvents.Clear();
        }

        public T Load<T>(Guid id, int version = 0) where T : class, IAggregate, new()
        {
            using (IDocumentSession session = store.OpenSession())
            {
                return session.Events.AggregateStream<T>(id, version);
            }

            throw new InvalidOperationException($"No aggregate by id {id}.");
        }
    }
}
