using System;

namespace SW.Checkout.Core.Aggregates
{
    public interface IAggregate
    {
        Guid Id { get; }
    }
}
