using System.Threading.Tasks;

namespace SW.Checkout.Core.Events
{
    public interface IEventBus

    {
        void Send<TEvent>(TEvent @event) where TEvent : IEvent;
    }

}
