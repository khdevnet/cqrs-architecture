using CQRS.Socks.Order.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;

namespace CQRS.Socks.Order.Client
{
    public class ClientWebApplicationFactory<TStartup>
        : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseDefaultServiceProvider(p => p.ValidateScopes = true)
                .ConfigureLogging(config =>
                {
                    config.ClearProviders();
                });
        }
    }
}
