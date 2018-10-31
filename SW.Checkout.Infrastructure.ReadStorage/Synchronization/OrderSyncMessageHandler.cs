using System;
using System.Linq;
using SW.Checkout.Core.Queues.ReadStorageSync;
using SW.Checkout.Domain.Orders.Events;
using SW.Checkout.Infrastructure.ReadStorage.Database;
using SW.Checkout.Read.ReadView;

namespace SW.Checkout.Infrastructure.ReadStorage.Synchronization
{
    internal class OrderSyncMessageHandler :
        IReadStorageSyncMessageHandler<OrderCreated>,
        IReadStorageSyncMessageHandler<OrderLineAdded>,
        IReadStorageSyncMessageHandler<OrderLineRemoved>,
        IReadStorageSyncMessageHandler<OrderItemQuantityAdded>,
        IReadStorageSyncMessageHandler<OrderItemQuantitySubtracted>

    {
        private readonly SwStoreReadDbContext db;

        public OrderSyncMessageHandler(
            SwStoreReadDbContext db)
        {
            this.db = db;
        }

        public void Handle(OrderCreated message)
        {
            if (IsOrderExist(message.OrderId))
            {
                return;
            }

            var orderView = new OrderReadView
            {
                Id = message.OrderId,
                Status = message.Status,
                CustomerId = message.CustomerId
            };
            db.OrderViews.Add(orderView);
            db.SaveChanges();
        }

        public void Handle(OrderLineAdded message)
        {
            if (!IsOrderExist(message.OrderId))
            {
                return;
            }
            var orderView = new OrderLineReadView
            {
                OrderId = message.OrderId,
                Status = message.Status,
                ProductId = message.ProductNumber,
                WarehouseId = message.WarehouseId,
                Quantity = message.Quantity
            };
            db.OrderLineViews.Add(orderView);
            db.SaveChanges();
        }

        public void Handle(OrderItemQuantityAdded message)
        {
            var orderItemView = db.OrderLineViews.FirstOrDefault(item => item.OrderId == message.OrderId && item.ProductId == message.ProductNumber);

            if (!IsOrderExist(message.OrderId) || orderItemView == null)
            {
                return;
            }

            orderItemView.Quantity += message.Quantity;

            db.SaveChanges();
        }

        public void Handle(OrderItemQuantitySubtracted message)
        {
            var orderItemView = db.OrderLineViews.FirstOrDefault(item => item.OrderId == message.OrderId && item.ProductId == message.ProductNumber);

            if (!IsOrderExist(message.OrderId) || orderItemView == null)
            {
                return;
            }

            orderItemView.Quantity -= message.Quantity;

            db.SaveChanges();
        }

        public void Handle(OrderLineRemoved message)
        {
            var orderItemView = db.OrderLineViews.FirstOrDefault(item => item.OrderId == message.OrderId && item.ProductId == message.ProductNumber);

            if (!IsOrderExist(message.OrderId) || orderItemView == null)
            {
                return;
            }

            db.OrderLineViews.Remove(orderItemView);
            db.SaveChanges();
        }

        private bool IsOrderExist(Guid id)
        {
            return db.OrderViews.Find(id) != null;
        }
    }
}
