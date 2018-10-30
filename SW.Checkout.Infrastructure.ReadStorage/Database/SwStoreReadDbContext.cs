using Microsoft.EntityFrameworkCore;
using SW.Store.Checkout.Read.ReadView;

namespace SW.Store.Checkout.Infrastructure.ReadStorage.Database
{
    public class SwStoreReadDbContext : DbContext
    {
        public DbSet<OrderReadView> OrderViews { get; set; }

        public DbSet<OrderLineReadView> OrderLineViews { get; set; }

        public DbSet<WarehouseReadView> WarehouseReadViews { get; set; }

        public DbSet<WarehouseItemReadView> WarehouseItemReadViews { get; set; }

        public SwStoreReadDbContext(DbContextOptions options)
            : base(options)
        {
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
        }
    }
}
