using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Domain.Orders.Commands;
using SW.Store.Checkout.Domain.Orders.Views;
using SW.Store.Checkout.Domain.Warehouses;
using SW.Store.Checkout.Domain.Warehouses.Views;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using SW.Store.Core.Events;
using SW.Store.Core.Messages;

namespace SW.Store.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler :
        IMessageHandler<CreateOrder>,
        IMessageHandler<AddOrderLine>,
        IMessageHandler<RemoveOrderLine>
    {
        private readonly IAggregationRepository repository;


        public OrderCommandHandler(
            IAggregationRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateOrder command)
        {
            if (IsOrderExist(command.OrderId))
            {
                return;
            }

            repository.Store(new OrderAggregate(command.OrderId, command.CustomerId));

            AddOrderLines(command.OrderId, command.Lines);
        }


        public void Handle(AddOrderLine command)
        {
            if (!IsOrderExist(command.OrderId))
            {
                return;
            }

            OrderLineDto[] lines = new[]{
                new OrderLineDto
                {
                    ProductNumber = command.ProductNumber,
                    Quantity = command.Quantity
                }};

            AddOrderLines(command.OrderId, lines);
        }

        public void Handle(RemoveOrderLine command)
        {
            if (!IsOrderExist(command.OrderId))
            {
                return;
            }

            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(command.OrderId);
            orderAggregate.RemoveLine(command.ProductNumber);
            repository.Store(orderAggregate);
        }

        private void AddOrderLines(Guid orderId, IEnumerable<OrderLineDto> orderLines)
        {
            IEnumerable<WarehouseView> warehouses = repository.Query<WarehouseView, WarehouseView>((w) => new WarehouseView { Id = w.Id, Items = w.Items });

            foreach (OrderLineDto orderLine in orderLines)
            {
                var transactions = new Dictionary<Guid, IEnumerable<IEvent>>();

                repository.Transaction(() =>
                {
                    WarehouseView warehouse = warehouses.FirstOrDefault(w => w.Items.Any(item => item.ProductId == orderLine.ProductNumber && item.Quantity >= orderLine.Quantity));

                    var orderItem = new OrderLine
                    {
                        ProductId = orderLine.ProductNumber,
                        Quantity = orderLine.Quantity
                    };

                    if (warehouse != null)
                    {
                        WarehouseAggregate warehouseAggregate = repository.Load<WarehouseAggregate>(warehouse.Id);
                        warehouseAggregate.SubstractItemQuantity(orderLine.ProductNumber, orderLine.Quantity);
                        transactions.Add(warehouseAggregate.Id, warehouseAggregate.PendingEvents);
                    }
                    else
                    {
                        orderItem.Status = OrderLineStatus.OutOfStock.ToString();
                    }
                    OrderAggregate orderAggregate = repository.Load<OrderAggregate>(orderId);
                    orderAggregate.AddLine(orderItem);
                    transactions.Add(orderAggregate.Id, orderAggregate.PendingEvents);

                    return transactions;
                });
            }
        }

        private bool IsOrderExist(Guid orderId)
        {
            return repository.FirstOrDefault<OrderView>(x => x.Id == orderId) != null;
        }
    }
}
