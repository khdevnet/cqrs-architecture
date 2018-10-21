using System.Threading.Tasks;

namespace SW.Store.Core.Events
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}
