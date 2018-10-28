using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SW.Store.Checkout.Domain.Orders.Commands;
using SW.Store.Checkout.Domain.Orders.Dto;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Client
{
    class Program
    {
        static string url = "http://localhost:33801";

        static void Main(string[] args)
        {
            IEnumerable<CreateOrder> createOrderModels = Enumerable.Range(0, 1)
      .Select(n => CreateOrderCommand()).ToList();
            var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (CreateOrder orderModel in createOrderModels)
            {
                actualOrderIds.Add(orderModel.OrderId);
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Create Order Id: " + orderModel.OrderId);
                    HttpResponseMessage response = client.PostAsJsonAsync($"{url}/api/v1/checkout", orderModel).Result;
                }
                CheckPendingOrders(actualOrderIds);
            }
            while (actualOrderIds.Any())
            {
                CheckPendingOrders(actualOrderIds);
            }

            Console.ReadKey();
        }

        private static void CheckPendingOrders(List<Guid> actualOrderIds)
        {
            if (actualOrderIds.Any())
            {
                actualOrderIds.RemoveAll(or => CheckOrderStatus(or));
            }
        }

        private static bool CheckOrderStatus(Guid orderId)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"{url}/api/v1/checkout/status/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    OrderReadView orderResponse = response.Content.ReadAsAsync<OrderReadView>().Result;
                    PrintOrderDetails(orderResponse);
                    return true;
                }
            }
            return false;
        }

        private static void PrintOrderDetails(OrderReadView orderResponse)
        {
            Console.WriteLine("Order Created Order Id: " + orderResponse.Id);
            foreach (var orderLine in orderResponse.Lines)
            {
                Console.WriteLine("Order Line: ");
                Console.WriteLine("-- ProductNumber: " + orderLine.ProductId);
                Console.WriteLine("-- ProductQuantity: " + orderLine.Quantity);
                Console.WriteLine("-- ProductStatus: " + orderLine.Status);
            }
            Console.WriteLine("=======================");
        }

        private static CreateOrder CreateOrderCommand()
        {
            return new CreateOrder()
            {
                OrderId = Guid.NewGuid(),
                CustomerId = 1,
                Lines = new[]
                 {
                     new OrderLineDto
                     {
                          ProductNumber = 1,
                          Quantity = 1
                     },
                     new OrderLineDto
                     {
                          ProductNumber = 2,
                          Quantity = 1
                     },
                     new OrderLineDto
                     {
                          ProductNumber = 3,
                          Quantity = 1
                     }
                 }
            };
        }
    }
}
