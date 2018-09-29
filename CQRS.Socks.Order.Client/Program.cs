using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CQRS.Socks.Order.Client.Extensibility.Models;

namespace CQRS.Socks.Order.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:51971";
            IEnumerable<CreateOrderRequestModel> createOrderModels = Enumerable.Range(0, 2)
      .Select(n => new CreateOrderRequestModel() { OrderId = Guid.NewGuid(), CustomerName = "Han Solo", CustomerAddress = "Stars" }).ToList();
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
    }
}
