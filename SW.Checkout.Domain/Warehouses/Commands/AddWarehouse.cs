using System;
using System.Collections.Generic;
using SW.Checkout.Core.Commands;

namespace SW.Checkout.Domain.Warehouses.Commands
{
    public class AddWarehouse : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<WarehouseItem> Items { get; set; } = new List<WarehouseItem>();
    }
}
