using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Read.ReadView;
using SW.Store.Core.Settings;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Database
{
    public class SwStoreReadDbContext : DbContext
    {
        private readonly IReadStorageConnectionStringProvider connectionStringProvider;

        public DbSet<OrderReadView> OrderViews { get; set; }

        public DbSet<OrderLineReadView> OrderLineViews { get; set; }

        public DbSet<WarehouseReadView> WarehouseReadViews { get; set; }

        public DbSet<WarehouseItemReadView> WarehouseItemReadViews { get; set; }

        public SwStoreReadDbContext(IReadStorageConnectionStringProvider connectionStringProvider)
            : base()
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BuidOrderEntities(modelBuilder);
            BuidWarehouseEntities(modelBuilder);
        }

        private static void BuidOrderEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderReadView>()
                            .HasKey(c => c.Id);

            modelBuilder.Entity<OrderReadView>()
                            .Property(b => b.Id)
                            .ValueGeneratedNever();

            modelBuilder.Entity<OrderReadView>()
                .HasMany(b => b.Lines)
                .WithOne(e => e.Order)
                .HasForeignKey(p => p.OrderId)
                .IsRequired();

            modelBuilder.Entity<OrderLineReadView>()
            .HasKey(c => c.Id);
        }

        private static void BuidWarehouseEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseReadView>()
                            .HasKey(c => c.Id);

            modelBuilder.Entity<WarehouseReadView>()
                            .Property(b => b.Id)
                            .ValueGeneratedNever();

            modelBuilder.Entity<WarehouseReadView>()
                .HasMany(b => b.Items)
                .WithOne(e => e.Warehouse)
                .HasForeignKey(p => p.WarehouseId)
                .IsRequired();

            modelBuilder.Entity<WarehouseItemReadView>()
            .HasKey(c => c.Id);

            var warehouses = new List<WarehouseReadView>
            {
                new WarehouseReadView { Id = Guid.Parse("6df8744a-d464-4826-91d1-08095ab49d93"), Name = "Naboo" },
                new WarehouseReadView { Id = Guid.Parse("6df8744a-d464-4826-91d1-08095ab49d94"), Name = "Tatooine" }
            };

            warehouses.ForEach(p => modelBuilder.Entity<WarehouseReadView>().HasData(p));

            foreach (WarehouseReadView warehouse in warehouses)
            {
                Enumerable.Range(1, 5)
                  .ToList()
                  .ForEach(productId =>
                              modelBuilder.Entity<WarehouseItemReadView>()
                              .HasData(new WarehouseItemReadView { Id = Guid.NewGuid(), ProductId = productId, WarehouseId = warehouse.Id, Quantity = 5000 }));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionStringProvider.Get());
        }
    }
}
