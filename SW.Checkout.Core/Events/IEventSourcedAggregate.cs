using System.Collections.Generic;
using SW.Store.Core.Events;

namespace SW.Store.Core.Aggregates
{
    public interface IEventSourcedAggregate : IAggregate
    {
        Queue<IEvent> PendingEvents { get; }
    }
}
