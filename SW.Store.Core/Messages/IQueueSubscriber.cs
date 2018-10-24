using System;

namespace SW.Store.Core.Messages
{
    public interface IQueueSubscriber : IDisposable 
    {
        void Subscribe();
    }
}
