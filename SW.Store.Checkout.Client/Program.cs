using SW.Store.Checkout.Client.Extensibility.Client;
using SW.Store.Checkout.Extensibility.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SW.Store.Checkout.Client
{
    class Program
    {
        static string url = "http://localhost:51971";

        static void Main(string[] args)
        {
            IEnumerable<OrderDto> createOrderModels = Enumerable.Range(0, 5000)
      .Select(n => CreateOrderRequestModel()).ToList();
            var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (OrderDto orderModel in createOrderModels)
            {
                actualOrderIds.Add(orderModel.OrderId);
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Create Order Id: " + orderModel.OrderId);
                    HttpResponseMessage response = client.PostAsJsonAsync($"{url}/api/checkout", orderModel).Result;
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
                HttpResponseMessage response = client.GetAsync($"{url}/api/checkout/status/{orderId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    CreateOrderResponseModel orderResponse = response.Content.ReadAsAsync<CreateOrderResponseModel>().Result;
                    PrintOrderDetails(orderResponse);
                    return true;
                }
            }
            return false;
        }

        private static void PrintOrderDetails(CreateOrderResponseModel orderResponse)
        {
            Console.WriteLine("Order Created Order Id: " + orderResponse.OrderId);
            foreach (var orderLine in orderResponse.Lines)
            {
                Console.WriteLine("Order Line: ");
                Console.WriteLine("-- ProductNumber: " + orderLine.ProductNumber);
                Console.WriteLine("-- ProductName: " + orderLine.ProductName);
                Console.WriteLine("-- ProductQuantity: " + orderLine.Quantity);
                Console.WriteLine("-- ProductStatus: " + orderLine.Status);
            }
            Console.WriteLine("=======================");
        }

        private static OrderDto CreateOrderRequestModel()
        {
            return new OrderDto()
            {
                OrderId = Guid.NewGuid(),
                CustomerName = "Han Solo",
                CustomerAddress = "Stars",
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
