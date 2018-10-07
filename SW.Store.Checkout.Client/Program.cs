using SW.Store.Checkout.Extensibility.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SW.Store.Checkout.Client
{
    class Program
    {
        static string url = "http://localhost:33801";

        static void Main(string[] args)
        {
            IEnumerable<CreateOrderRequestModel> createOrderModels = Enumerable.Range(0, 500)
      .Select(n => CreateOrderRequestModel()).ToList();
            var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (CreateOrderRequestModel orderModel in createOrderModels)
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
                    OrderResponseModel orderResponse = response.Content.ReadAsAsync<OrderResponseModel>().Result;
                    PrintOrderDetails(orderResponse);
                    return true;
                }
            }
            return false;
        }

        private static void PrintOrderDetails(OrderResponseModel orderResponse)
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

        private static CreateOrderRequestModel CreateOrderRequestModel()
        {
            return new CreateOrderRequestModel()
            {
                OrderId = Guid.NewGuid(),
                CustomerName = "Han Solo",
                CustomerAddress = "Stars",
                Lines = new[]
                 {
                     new CreateOrderLineRequestModel
                     {
                          ProductNumber = 1,
                          Quantity = 1
                     },
                     new CreateOrderLineRequestModel
                     {
                          ProductNumber = 2,
                          Quantity = 1
                     },
                     new CreateOrderLineRequestModel
                     {
                          ProductNumber = 3,
                          Quantity = 1
                     }
                 }
            };
        }
    }
}
