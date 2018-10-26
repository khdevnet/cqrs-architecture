using System;
using SW.Store.Checkout.Domain.Orders.Events;
using SW.Store.Checkout.Extensibility.Queues.ReadStorageSync;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Synchronization
{
    internal class OrderSyncMessageHandler :
        IReadStorageSyncMessageHandler<OrderCreated>,
        IReadStorageSyncMessageHandler<OrderLineAdded>
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
                Status = message.Status
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
                Quantity = message.Quantity
            };
            db.OrderLineViews.Add(orderView);
            db.SaveChanges();
        }


        private bool IsOrderExist(Guid id)
        {
            return db.OrderViews.Find(id) != null;
        }
    }
}
