using System;
using System.Collections.Generic;
using System.Linq;
using SW.Checkout.Core.Aggregates;
using SW.Checkout.Core.Events;
using SW.Checkout.Core.Messages;
using SW.Checkout.Core.Queues.ProcessOrder;
using SW.Checkout.Domain.Orders.Commands;
using SW.Checkout.Domain.Orders.Dto;
using SW.Checkout.Domain.Orders.Enum;
using SW.Checkout.Domain.Orders.Views;
using SW.Checkout.Domain.Warehouses;
using SW.Checkout.Domain.Warehouses.Views;

namespace SW.Checkout.Domain.Orders.Handlers
{
    public class OrderCommandHandler :
        IMessageHandler<CreateOrder>,
        IMessageHandler<AddOrderLine>,
        IMessageHandler<RemoveOrderLine>,
        IMessageHandler<AddOrderItemQuantity>,
        IMessageHandler<SubtractOrderItemQuantity>
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
            if (IsOrderExist(command.OrderId))
            {
                return;
            }

            var createOrderEvents = new Dictionary<Guid, List<IEvent>>();

            var order = new OrderAggregate(command.OrderId, command.CustomerId);
            createOrderEvents.Add(order.Id, order.PendingEvents.ToList());

            Dictionary<Guid, List<IEvent>> orderLinesEvents = AddOrderLines(command.OrderId, command.Lines);

            foreach (KeyValuePair<Guid, List<IEvent>> lineAgg in orderLinesEvents)
            {
                AddAggEvents(createOrderEvents, lineAgg.Key, lineAgg.Value);
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

            var aggEvents = AddOrderLines(command.OrderId, lines);
            Func<Dictionary<Guid, List<IEvent>>> transactionFunc = () => AddOrderLines(command.OrderId, lines);
            Action transactionPostProcessFunc = () => aggEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));

            repository.Transaction(transactionFunc, transactionPostProcessFunc);
        }

        public void Handle(AddOrderItemQuantity command)
        {
            if (!IsOrderExist(command.OrderId))
            {
                return;
            }

            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(command.OrderId);

            orderAggregate.AddItemQuantity(command.ProductNumber, command.Quantity);

            var aggEvents = new Dictionary<Guid, List<IEvent>>() { { orderAggregate.Id, orderAggregate.PendingEvents.ToList() } };

            Func<Dictionary<Guid, List<IEvent>>> transactionFunc = () => aggEvents;
            Action transactionPostProcessFunc = () => aggEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));
            repository.Transaction(transactionFunc, transactionPostProcessFunc);
        }

        public void Handle(SubtractOrderItemQuantity command)
        {
            if (!IsOrderExist(command.OrderId))
            {
                return;
            }

            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(command.OrderId);

            orderAggregate.SubtractItemQuantity(command.ProductNumber, command.Quantity);

            var aggEvents = new Dictionary<Guid, List<IEvent>>() { { orderAggregate.Id, orderAggregate.PendingEvents.ToList() } };

            Func<Dictionary<Guid, List<IEvent>>> transactionFunc = () => aggEvents;
            Action transactionPostProcessFunc = () => aggEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));
            repository.Transaction(transactionFunc, transactionPostProcessFunc);
        }

        public void Handle(RemoveOrderLine command)
        {
            if (!IsOrderExist(command.OrderId))
            {
                return;
            }

            OrderAggregate orderAggregate = repository.Load<OrderAggregate>(command.OrderId);
            orderAggregate.RemoveLine(command.ProductNumber);
            var aggEvents = new Dictionary<Guid, List<IEvent>>() { { command.OrderId, orderAggregate.PendingEvents.ToList() } };
            Func<Dictionary<Guid, List<IEvent>>> transactionFunc = () => aggEvents;
            Action transactionPostProcessFunc = () => aggEvents.SelectMany(agg => agg.Value).ToList().ForEach(@event => readStorageSyncEventBus.Send(@event));

            repository.Transaction(transactionFunc, transactionPostProcessFunc);
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
                    AddAggEvents(events, warehouseAggregate.Id, warehouseAggregate.PendingEvents.ToList());
                    orderItem.WarehouseId = warehouseAggregate.Id;
                }
                else
                {
                    orderItem.Status = OrderLineStatus.OutOfStock.ToString();
                }

                OrderAggregate orderAggregate = repository.Load<OrderAggregate>(orderId);
                orderAggregate.AddLine(orderItem);

                AddAggEvents(events, orderAggregate.Id, orderAggregate.PendingEvents.ToList());
            }
            return events;
        }

        private static void AddAggEvents(Dictionary<Guid, List<IEvent>> aggEvents, Guid id, List<IEvent> pendingEvents)
        {
            if (aggEvents.ContainsKey(id))
            {
                aggEvents[id].AddRange(pendingEvents.ToList());
            }
            else
            {
                aggEvents.Add(id, pendingEvents.ToList());
            }
        }

        private bool IsOrderExist(Guid orderId)
        {
            return repository.FirstOrDefault<OrderView>(x => x.Id == orderId) != null;
        }
    }
}
