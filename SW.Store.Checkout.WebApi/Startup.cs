using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Checkout.Domain.Accounts.Commands;
using SW.Store.Checkout.Domain.Orders.Handlers;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Service;
using SW.Store.Core;
using SW.Store.Core.Commands;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning();
            ConfigureMediator(services);
            //string connectionString = Configuration.GetSection("EventStore").GetValue<string>("ConnectionString");
            //services.ConfigureMarten(connectionString);
            //string connection = Configuration.GetConnectionString(nameof(SwStoreDbContext));
            //services.RegisterDbContext(connection);
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType<SwStoreDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<FakeLogger>().As<ILogger>();
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterType<CommandBusSettingsProvider>().As<ICommandBusSettingsProvider>();

            builder.RegisterModule<CoreAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<ServiceAutofacModule>();
            builder.RegisterModule<RabbitMQAutofacModule>();
        }

        private static void ConfigureMediator(IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
