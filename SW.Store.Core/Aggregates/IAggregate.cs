using System;

namespace SW.Store.Core.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
