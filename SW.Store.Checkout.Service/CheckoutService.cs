using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SW.Store.Checkout.Service
{
    internal class CheckoutService : ICheckoutService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICrudRepository<Product, int> productRepository;
        private readonly IWarehouseRepository warehouseRepository;
        private readonly IWarehouseItemRepository warehouseItemRepository;

        public CheckoutService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ICrudRepository<Product, int> productRepository,
            IWarehouseRepository warehouseRepository,
            IWarehouseItemRepository warehouseItemRepository)
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.warehouseRepository = warehouseRepository;
            this.warehouseItemRepository = warehouseItemRepository;
        }

        public Guid ProcessOrder(OrderDto order)
        {
            using (var scope = new TransactionScope())
            {
                var orderEntity = new Order
                {
                    Id = order.OrderId,
                    Customer = customerRepository.Get(order.CustomerName, order.CustomerAddress)
                };

                IEnumerable<Warehouse> warehouses = warehouseRepository.Get(nameof(Warehouse.Items));

                foreach (OrderLineDto orderLine in order.Lines)
                {
                    var orderLineEntity = new OrderLine
                    {
                        Product = productRepository.GetById(orderLine.ProductNumber),
                        Quantity = orderLine.Quantity
                    };

                    Warehouse warehouse = warehouses
                        .FirstOrDefault(w => w.Items.Where(it => it.ProductId == orderLine.ProductNumber).Sum(it => it.Quantity) >= orderLine.Quantity);
                    if (warehouse != null)
                    {
                        orderLineEntity.Warehouse = warehouse;
                        UpdateWarehouseItemQuantity(orderLine.ProductNumber, warehouse.Id, orderLine.Quantity);
                    }
                    else
                    {
                        orderLineEntity.LineStatus = OrderLineStatus.OutOfStock;
                    }

                    orderEntity.Lines.Add(orderLineEntity);
                };

                orderRepository.Add(orderEntity);
                scope.Complete();
                return orderEntity.Id;
            }
        }

        private void UpdateWarehouseItemQuantity(int productId, int warehouseId, int orderLineQuantity)
        {
            WarehouseItem warehouseItem = warehouseItemRepository.Get(productId, warehouseId);

            int availableProductQuantity = warehouseItem.Quantity -= orderLineQuantity;
            warehouseItemRepository.UpdateQuantity(productId, warehouseId, availableProductQuantity);
        }
    }
}
