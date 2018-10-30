using System;

namespace SW.Checkout.Core.Messages
{
    public interface IQueueSubscriber : IDisposable 
    {
        void Subscribe();
    }
}
