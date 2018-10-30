using System;

namespace SW.Checkout.Core.Replication
{
    public interface IReplicationManager
    {
        void Replicate(DateTime? timestamp = null);
    }
}
