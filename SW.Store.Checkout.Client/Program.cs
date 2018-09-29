using SW.Store.Checkout.Client.Extensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SW.Store.Checkout.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:51971";
            IEnumerable<CreateOrderRequestModel> createOrderModels = Enumerable.Range(0, 2)
      .Select(n => CreateOrderRequestModel()).ToList();
            var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (CreateOrderRequestModel orderModel in createOrderModels)
            {
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Create Order Id: " + orderModel.OrderId);
                    HttpResponseMessage response = client.PostAsJsonAsync($"{url}/api/orders", orderModel).Result;
                    actualOrderIds.Add(response.Content.ReadAsAsync<CreateOrderResponseModel>().Result.OrderId);
                }
            }

            while (expectedOrderIds.Any())
            {
                foreach (Guid item in actualOrderIds)
                {
                    int index = expectedOrderIds.IndexOf(item);
                    if (index > -1)
                    {
                        Console.WriteLine("Order Created Order Id: " + item);
                        expectedOrderIds.RemoveAt(index);
                    }
                }
            }

            Console.ReadKey();
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
                     new OrderLineRequestModel
                     {
                          ProductNumber = 1,
                          Quantity = 1
                     },
                     new OrderLineRequestModel
                     {
                          ProductNumber = 2,
                          Quantity = 1
                     }
                 }
            };
        }
    }
}
