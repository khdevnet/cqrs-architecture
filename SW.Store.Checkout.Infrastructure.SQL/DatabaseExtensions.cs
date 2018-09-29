using System;
using System.Collections.Generic;
using System.Text;
using SW.Store.Checkout.Infrastructure.SQL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SW.Store.Checkout.Infrastructure.SQL
{
    public static class DatabaseExtensions
    {
        public static void RegisterDbContext(this IServiceCollection collection, string connection)
        {
            collection.AddDbContext<SocksShopDbContext>
                (options => options.UseSqlServer(connection));
        }
    }
}
