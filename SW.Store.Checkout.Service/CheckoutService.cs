using System;
using System.Collections.Generic;
using System.Transactions;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Domain.Extensibility;
using SW.Store.Checkout.Extensibility;
using SW.Store.Checkout.Extensibility.Dto;

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

        public Guid CreateOrder(Guid orderId, string customerName, string customerAddress, IEnumerable<OrderLineDto> orderLines)
        {
            Order order = orderRepository.GetById(orderId);
            if (order != null)
            {
                return Guid.Empty;
            }

            using (var scope = new TransactionScope())
            {
                var orderEntity = new Order
                {
                    Id = orderId,
                    CustomerId = customerRepository.Get(customerName, customerAddress).Id //  customerRepository.Get(order.CustomerName, order.CustomerAddress)
                };

                foreach (OrderLineDto orderLine in orderLines)
                {
                    var orderLineEntity = new OrderLine
                    {
                        ProductId = orderLine.ProductNumber, //orderLine.ProductNumber
                        Quantity = orderLine.Quantity
                    };

                    orderLineEntity.Warehouse = warehouseRepository.Get(orderLine.ProductNumber, orderLine.Quantity); //orderLine.ProductNumber
                    if (orderLineEntity.Warehouse != null)
                    {
                        UpdateWarehouseItemQuantity(orderLine.ProductNumber, orderLineEntity.Warehouse.Id, orderLine.Quantity); //ProductNumber
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
