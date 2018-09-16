using CQRS.Socks.Order.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace CQRS.Socks.Order.Tests
{
    public class ControllerTestsBase
           : IClassFixture<WebApplicationFactory<Startup>>, IDisposable
    {
        protected readonly WebApplicationFactory<Startup> webApplicationFactory;
        protected readonly HttpClient client;

        public ControllerTestsBase(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
            client = webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            webApplicationFactory?.Dispose();
            client?.Dispose();
        }
    }

}
