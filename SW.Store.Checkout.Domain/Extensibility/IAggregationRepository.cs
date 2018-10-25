using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SW.Store.Core.Aggregates;
using SW.Store.Core.Events;

namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IAggregationRepository
    {
        void Store(EventSourcedAggregate aggregate);

        void Transaction(Func<Dictionary<Guid, IEnumerable<IEvent>>> transasction);

        IEnumerable<TResult> Query<TSource, TResult>(Expression<Func<TSource, TResult>> selector);

        TSource FirstOrDefault<TSource>(Expression<Func<TSource, bool>> predicate);

        T Load<T>(Guid id, int version = 0) where T : class, IAggregate, new();
    }
}
