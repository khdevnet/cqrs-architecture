using System;
using System.Collections.Generic;
using System.Linq;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Domain.Orders.Commands;
using SW.Store.Checkout.Domain.Orders.Dto;
using SW.Store.Checkout.Domain.Orders.Enum;
using SW.Store.Checkout.Domain.Orders.Views;
using SW.Store.Checkout.Domain.Warehouses;
using SW.Store.Checkout.Domain.Warehouses.Views;
using SW.Store.Core.Events;
using SW.Store.Core.Messages;
using SW.Store.Core.Queues.ProcessOrder;

namespace SW.Store.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler :
        IMessageHandler<CreateOrder>,
        IMessageHandler<AddOrderLine>,
        IMessageHandler<RemoveOrderLine>
    {
        private readonly IAggregationRepository repository;
        private readonly IReadStorageSyncEventBus readStorageSyncEventBus;

        public OrderCommandHandler(
            IAggregationRepository repository,
            IReadStorageSyncEventBus readStorageSyncEventBus)
        {
            this.repository = repository;
            this.readStorageSyncEventBus = readStorageSyncEventBus;
        }

        public void Handle(CreateOrder command)
        {
            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(command.OrderId);
            if (orderAggregate.Status != OrderStatus.NotExist.ToString())
            {
                return;
            }

            var createOrderEvents = new Dictionary<Guid, List<IEvent>>();

            var order = new OrderAggregate(command.OrderId, command.CustomerId);
            createOrderEvents.Add(order.Id, order.PendingEvents.ToList());

            Dictionary<Guid, List<IEvent>> orderLinesEvents = AddOrderLines(command.OrderId, command.Lines);

            foreach (KeyValuePair<Guid, List<IEvent>> lineAgg in orderLinesEvents)
            {
                if (createOrderEvents.ContainsKey(lineAgg.Key))
                {
                    createOrderEvents[lineAgg.Key].AddRange(lineAgg.Value);
                }
                else
                {
                    createOrderEvents.Add(lineAgg.Key, lineAgg.Value.ToList());
                }
            }

            Func<Dictionary<Guid, List<IEvent>>> transactionFunc = () => createOrderEvents;
            Action transactionPostProcessFunc = () => createOrderEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));

            repository.Transaction(transactionFunc, transactionPostProcessFunc);

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

            repository.Transaction(() => AddOrderLines(command.OrderId, lines));
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

        private Dictionary<Guid, List<IEvent>> AddOrderLines(Guid orderId, IEnumerable<OrderLineDto> orderLines)
        {
            IEnumerable<WarehouseView> warehouses = repository.Query<WarehouseView, WarehouseView>((w) => new WarehouseView { Id = w.Id, Items = w.Items });
            var events = new Dictionary<Guid, List<IEvent>>();
            foreach (OrderLineDto orderLine in orderLines)
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
                    events.Add(warehouseAggregate.Id, warehouseAggregate.PendingEvents.ToList());
                }
                else
                {
                    orderItem.Status = OrderLineStatus.OutOfStock.ToString();
                }
                OrderAggregate orderAggregate = repository.Load<OrderAggregate>(orderId);
                orderAggregate.AddLine(orderItem);
                events.Add(orderAggregate.Id, orderAggregate.PendingEvents.ToList());
            }
            return events;
        }

        private bool IsOrderExist(Guid orderId)
        {
            return repository.FirstOrDefault<OrderView>(x => x.Id == orderId) != null;
        }
    }
}
