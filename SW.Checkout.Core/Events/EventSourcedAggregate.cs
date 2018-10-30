using System;
using System.Collections.Generic;
using SW.Checkout.Core.Events;

namespace SW.Checkout.Core.Aggregates
{
    public abstract class EventSourcedAggregate : IEventSourcedAggregate
    {
        public Guid Id { get; protected set; }

        public Queue<IEvent> PendingEvents { get; private set; }

        protected EventSourcedAggregate()
        {
            PendingEvents = new Queue<IEvent>();
        }

        protected void Append(IEvent @event)
        {
            PendingEvents.Enqueue(@event);
        }
    }
}
