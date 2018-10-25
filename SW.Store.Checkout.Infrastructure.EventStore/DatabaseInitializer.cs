using System;
using System.Linq;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Domain.Warehouses;
using SW.Store.Core;

namespace SW.Store.Checkout.Infrastructure.EventStore
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly IAggregationRepository repository;

        public int Order { get; } = 2;

        public DatabaseInitializer(IAggregationRepository repository)
        {
            this.repository = repository;
        }

        public void Init()
        {
            repository.Store(CreateWarehouseAggregate(Guid.NewGuid(), "Naboo"));
            repository.Store(CreateWarehouseAggregate(Guid.NewGuid(), "Tatooine"));
        }

        private static WarehouseAggregate CreateWarehouseAggregate(Guid warehouseId, string name)
        {
            return new WarehouseAggregate(warehouseId, name, Enumerable.Range(1, 5).Select(productId => new WarehouseItem
            {
                ProductId = productId,
                Quantity = 5000
            }));
        }
    }
}
