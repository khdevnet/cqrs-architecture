using System.Threading.Tasks;

namespace SW.Store.Core.Events
{
    public interface IEventBus

    {
        void Send<TEvent>(TEvent @event) where TEvent : IEvent;
    }

}
