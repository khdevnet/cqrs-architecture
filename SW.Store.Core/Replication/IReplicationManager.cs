using System;

namespace SW.Store.Core.Replication
{
    public interface IReplicationManager
    {
        void Replicate(DateTime? timestamp = null);
    }
}
