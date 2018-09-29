using Autofac;
using SW.Store.Checkout.Infrastructure.SQL;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using SW.Store.Checkout.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            string connection = Configuration.GetConnectionString("SocksShopDatabase");
            services.RegisterDbContext(connection);
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SQLAutofacModule>();
            builder.RegisterModule<ServiceAutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    SocksShopDbContext context = serviceScope.ServiceProvider.GetRequiredService<SocksShopDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                }
            }

            app.UseMvc();
        }
    }
}
