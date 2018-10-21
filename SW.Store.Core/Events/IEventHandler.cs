using MediatR;

namespace SW.Store.Core.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
           where TEvent : IEvent
    {
    }
}
