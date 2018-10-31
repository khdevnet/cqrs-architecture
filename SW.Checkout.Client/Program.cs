using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using SW.Checkout.Client.Models;

namespace SW.Checkout.Client
{
    class Program
    {
        static string url = "http://localhost:33801";

        static void Main(string[] args)
        {
            IEnumerable<CreateOrderRequest> createOrderModels = Enumerable.Range(0, 500)
      .Select(n => CreateOrderCommand()).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (CreateOrderRequest orderModel in createOrderModels)
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync($"{url}/api/v1/checkout", orderModel).Result;
                    var createdOrderId = response.Content.ReadAsAsync<Guid>().Result;
                    Console.WriteLine("Create Order Id: " + createdOrderId);
                    actualOrderIds.Add(createdOrderId);

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
                    OrderResponseModel orderResponse = response.Content.ReadAsAsync<OrderResponseModel>().Result;
                    PrintOrderDetails(orderResponse);
                    return true;
                }
            }
            return false;
        }

        private static void PrintOrderDetails(OrderResponseModel orderResponse)
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

        private static CreateOrderRequest CreateOrderCommand()
        {
            return new CreateOrderRequest()
            {
                CustomerId = 1,
                Lines = new[]
                 {
                     new OrderLineRequestModel
                     {
                          ProductNumber = 1,
                          Quantity = 1
                     },
                     new OrderLineRequestModel
                     {
                          ProductNumber = 2,
                          Quantity = 1
                     },
                     new OrderLineRequestModel
                     {
                          ProductNumber = 3,
                          Quantity = 1
                     }
                 }
            };
        }
    }
}
