using CQRS.Socks.Order.Domain;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Socks.Order.Infrastructure.SQL.Database
{
    public class SocksShopDbContext : DbContext
    {

        public SocksShopDbContext(DbContextOptions<SocksShopDbContext> options) : base(options) { }

        public DbSet<Domain.Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
