using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Core.Aggregates
{
    public interface IAggregationRepository
    {
        void Store(EventSourcedAggregate aggregate);

        void Transaction(Func<Dictionary<Guid, List<IEvent>>> transasction, Action postProcess = null);

        IEnumerable<TResult> Query<TSource, TResult>(Expression<Func<TSource, TResult>> selector);

        TSource FirstOrDefault<TSource>(Expression<Func<TSource, bool>> predicate);

        IEnumerable<IEvent> GetEvents(Guid streamId, int version = 0, DateTime? timestamp = null);

        T Load<T>(Guid id, int version = 0, DateTime? timestamp = null) where T : class, IAggregate, new();
    }
}
