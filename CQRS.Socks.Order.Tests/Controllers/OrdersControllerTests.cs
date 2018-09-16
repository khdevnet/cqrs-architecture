using CQRS.Socks.Order.Tests.Comparers;
using CQRS.Socks.Order.WebApi;
using CQRS.Socks.Order.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CQRS.Socks.Order.Tests
{
    public class OrdersControllerTests
           : ControllerTestsBase
    {
        public OrdersControllerTests(WebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory)
        {

        }

        [Fact]
        public async Task CreateOrder()
        {

            IEnumerable<CreateOrderModel> createOrderModels = Enumerable.Range(0, 99)
                .Select(n => new CreateOrderModel() { OrderId = Guid.NewGuid(), CustomerName = "TestUser1" });
            IEnumerable<Guid> expectedOrderIds = createOrderModels.Select(o => o.OrderId);

            Task<HttpResponseMessage>[] responseTasks = createOrderModels.Select(orderModel => client.PostAsJsonAsync("/api/orders", orderModel)).ToArray();
            HttpResponseMessage[] responses = await Task.WhenAll(responseTasks);

            IEnumerable<Task<CreateOrderResponseModel>> actualOrders = responses.Select(r => r.Content.ReadAsAsync<CreateOrderResponseModel>());
            CreateOrderResponseModel[] ordersModels = await Task.WhenAll(actualOrders);
            IEnumerable<Guid> actualOrderIds = ordersModels.Select(o => o.OrderId);

            Assert.True(expectedOrderIds.OrderBy(x => x).SequenceEqual(actualOrderIds.OrderBy(x => x), new GuidEqualityComparer()));

        }

    }

}
