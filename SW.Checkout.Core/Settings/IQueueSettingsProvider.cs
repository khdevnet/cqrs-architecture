using SW.Checkout.Core.Settings.Dto;

namespace SW.Checkout.Core.Settings
{
    public interface IQueueSettingsProvider
    {
        QueueSettings Get();
    }
}
