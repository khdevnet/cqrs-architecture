using System;
using SW.Store.Core.Aggregates;

namespace SW.Store.Checkout.Domain.Extensibility
{
    public interface IAggregationRepository
    {
        void Store(EventSourcedAggregate aggregate);

        T Load<T>(Guid id, int version = 0) where T : class, IAggregate, new();
    }
}
