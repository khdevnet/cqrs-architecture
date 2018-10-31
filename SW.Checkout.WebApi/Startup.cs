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
using SW.Checkout.Core;
using SW.Checkout.Core.Initializers;
using SW.Checkout.Domain;
using SW.Checkout.Infrastructure.EventStore;
using SW.Checkout.Infrastructure.RabbitMQ;
using SW.Checkout.Infrastructure.ReadStorage;
using SW.Checkout.Infrastructure.ReadStorage.Database;

namespace SW.Checkout.WebApi
{
    public class Startup
    {
        private const string CorsPolicyName = "CorsPolicy";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddApiVersioning();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    CorsPolicyName,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddDbContext<SwStoreReadDbContext>(options => options.UseNpgsql(Configuration.GetSection("ReadStorage")["ConnectionString"]));

        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<FakeLogger>().As<ILogger>();

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

            app.UseCors(CorsPolicyName);
            app.UseMvc();

            //IEnumerable<IInitializer> initializers = app.ApplicationServices.GetService<IEnumerable<IInitializer>>();
            //RunInitializers(initializers);
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
