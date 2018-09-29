using SW.Store.Checkout.Domain;
using Microsoft.EntityFrameworkCore;

namespace SW.Store.Checkout.Infrastructure.SQL.Database
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, Name = "Han Solo", ShippingAddress = "Stars" });

            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "R2-D2" });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 2, Name = "Speeder" });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 3, Name = "BB-8" });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 4, Name = "Blaster" });
            modelBuilder.Entity<Product>().HasData(new Product { Id = 5, Name = "Death star" });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
