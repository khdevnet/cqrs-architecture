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
        static void Main(string[] args)
        {
            string url = "http://localhost:51971";
            IEnumerable<OrderDto> createOrderModels = Enumerable.Range(0, 5000)
      .Select(n => CreateOrderRequestModel()).ToList();
            var expectedOrderIds = createOrderModels.Select(o => o.OrderId).ToList();
            var actualOrderIds = new List<Guid>();
            foreach (OrderDto orderModel in createOrderModels)
            {
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Create Order Id: " + orderModel.OrderId);
                    HttpResponseMessage response = client.PostAsJsonAsync($"{url}/api/orders", orderModel).Result;
                    var orderResponse = response.Content.ReadAsAsync<CreateOrderResponseModel>().Result;
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
            }

            //while (expectedOrderIds.Any())
            //{
            //    foreach (Guid item in actualOrderIds)
            //    {
            //        int index = expectedOrderIds.IndexOf(item);
            //        if (index > -1)
            //        {
            //            Console.WriteLine("Order Created Order Id: " + item);
            //            expectedOrderIds.RemoveAt(index);
            //        }
            //    }
            //}

            Console.ReadKey();
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
