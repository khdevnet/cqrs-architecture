using CQRS.Socks.Order.WebApi;
using CQRS.Socks.Order.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CQRS.Socks.Order.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var webFactory = new ClientWebApplicationFactory<Startup>())
            {

                IEnumerable<CreateOrderModel> createOrderModels = Enumerable.Range(0, 99)
          .Select(n => new CreateOrderModel() { OrderId = Guid.NewGuid(), CustomerName = "TestUser1" }).ToList();
                var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
                var actualOrderIds = new List<Guid>();
                foreach (CreateOrderModel orderModel in createOrderModels)
                {
                    using (HttpClient client = webFactory.CreateClient())
                    {
                        Console.WriteLine("Create Order Id: " + orderModel.OrderId);
                        HttpResponseMessage response = client.PostAsJsonAsync("/api/orders", orderModel).Result;
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

            }
            Console.ReadKey();
        }
    }
}
