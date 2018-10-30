using System.Collections.Generic;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Core.Aggregates
{
    public interface IEventSourcedAggregate : IAggregate
    {
        Queue<IEvent> PendingEvents { get; }
    }
}
