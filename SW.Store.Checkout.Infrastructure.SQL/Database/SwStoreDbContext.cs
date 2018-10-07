using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Domain;
using SW.Store.Core;
using System.Collections.Generic;

namespace SW.Store.Checkout.Infrastructure.SQL.Database
{
    public class SwStoreDbContext : DbContext
    {
        private readonly IConnectionStringProvider connectionStringProvider;

        //public SwStoreDbContext(DbContextOptions<SwStoreDbContext> options) : base(options) { }

        public SwStoreDbContext(IConnectionStringProvider connectionStringProvider) : base()
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<WarehouseItem> WarehouseItems { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseItem>()
                .HasKey(wi => new { wi.ProductId, wi.WarehouseId });

            modelBuilder.Entity<Customer>().HasData(new Customer { Id = 1, Name = "Han Solo", ShippingAddress = "Stars" });

            var products = new List<Product>
            {
                new Product { Id = 1, Name = "R2-D2" },
                new Product { Id = 2, Name = "Speeder" },
                new Product { Id = 3, Name = "BB-8" },
                new Product { Id = 4, Name = "Blaster" },
                new Product { Id = 5, Name = "Death star" }
            };

            products.ForEach(p => modelBuilder.Entity<Product>().HasData(p));

            var warehouses = new List<Warehouse>
            {
                new Warehouse { Id = 1, Name = "Naboo" },
                new Warehouse { Id = 2, Name = "Tatooine" }
            };

            warehouses.ForEach(p => modelBuilder.Entity<Warehouse>().HasData(p));

            foreach (Warehouse warehouse in warehouses)
            {
                foreach (Product product in products)
                {
                    modelBuilder.Entity<WarehouseItem>().HasData(new WarehouseItem { ProductId = product.Id, WarehouseId = warehouse.Id, Quantity = 5000 });
                }
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStringProvider.Get());
        }
    }
}
