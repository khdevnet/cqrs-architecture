using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using AutoMapper;
using Csrh.TimeReporting.Infrastructure.DataAccess.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SW.Store.Checkout.Infrastructure.EventStore;
using SW.Store.Checkout.Infrastructure.RabbitMQ;
using SW.Store.Core;
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
            builder.RegisterType<ConnectionStringProvider>().As<IConnectionStringProvider>();
            builder.RegisterType<CommandBusSettingsProvider>().As<ICommandBusSettingsProvider>();

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
            RunInitializers(app.ApplicationServices);
        }

        public static void RunInitializers(IServiceProvider applicationServices)
        {
            IEnumerable<IInitializer> initializers = applicationServices.GetService<IEnumerable<IInitializer>>();

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
