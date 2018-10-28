using System.Collections.Generic;
using System.Linq;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Checkout.Domain;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Checkout.Infrastructure.ReadStorage;
using SW.Store.Checkout.Infrastructure.ReadStorage.Database;
using SW.Store.Core;
using SW.Store.Core.Initializers;
using SW.Store.Core.Queues.ProcessOrder;
using SW.Store.Core.Queues.ReadStorageSync;
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
            services.AddDbContext<SwStoreReadDbContext>(options => options.UseNpgsql(Configuration.GetSection("ReadStorage")["ConnectionString"]));
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<FakeLogger>().As<ILogger>();
            builder.RegisterType<EventStoreConnectionStringProvider>().As<IEventStoreConnectionStringProvider>();
            builder.RegisterType<ReadStorageConnectionStringProvider>().As<IReadStorageConnectionStringProvider>();
            builder.RegisterType<ProcessOrderQueueSettingsProvider>().As<IProcessOrderQueueSettingsProvider>();
            builder.RegisterType<ReadStorageSyncQueueSettingsProvider>().As<IReadStorageSyncQueueSettingsProvider>();

            builder.RegisterModule<DomainAutofacModule>();
            builder.RegisterModule<CoreAutofacModule>();
            builder.RegisterModule<EventStoreAutofacModule>();
            builder.RegisterModule<RabbitMQAutofacModule>();
            builder.RegisterModule<ReadStorageAutofacModule>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            IEnumerable<IInitializer> initializers = app.ApplicationServices.GetService<IEnumerable<IInitializer>>();
            RunInitializers(initializers);
        }

        public static void RunInitializers(IEnumerable<IInitializer> initializers)
        {
            if (initializers != null)
            {
                initializers
                    .OrderBy(i => i.Order)
                    .ToList()
                    .ForEach(initializer => initializer.Init());
            }
        }
    }
}
