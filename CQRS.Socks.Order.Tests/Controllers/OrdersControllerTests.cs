using CQRS.Socks.Order.WebApi;
using CQRS.Socks.Order.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
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
            HttpResponseMessage response = await client
                .PostAsJsonAsync("/api/orders", new CreateOrderModel() { OrderId = Guid.NewGuid(), CustomerName = "TestUser1" });

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }

}
