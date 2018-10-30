using SW.Checkout.Core.Messages;

namespace SW.Checkout.Core.Queues.ReadStorageSync
{

    public interface IReadStorageSyncMessageHandler<in TMessage> : IMessageHandler<TMessage>, IReadStorageSyncMessageHandler
        where TMessage : IMessage
    {
    }

    public interface IReadStorageSyncMessageHandler : IMessageHandler
    {
    }
}
