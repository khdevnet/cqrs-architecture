using Microsoft.EntityFrameworkCore;
using SW.Checkout.Read.ReadView;

namespace SW.Checkout.Infrastructure.ReadStorage.Database
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
            BuildWarehouseEntities(modelBuilder);
        }

        private static void BuidOrderEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderReadView>().ToTable("order_read_view");

            modelBuilder.Entity<OrderReadView>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<OrderReadView>()
                        .Property(b => b.Id)
                        .HasColumnName("id")
                        .ValueGeneratedNever();

            modelBuilder.Entity<OrderReadView>()
                        .Property(b => b.CustomerId)
                        .HasColumnName("customer_id");

            modelBuilder.Entity<OrderReadView>()
                        .Property(b => b.Status)
                        .HasColumnName("status");

            modelBuilder.Entity<OrderReadView>()
                .HasMany(b => b.Lines)
                .WithOne(e => e.Order)
                .HasForeignKey(p => p.OrderId)
                .IsRequired();


            modelBuilder.Entity<OrderLineReadView>()
                        .ToTable("order_line_read_view");

            modelBuilder.Entity<OrderLineReadView>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<OrderLineReadView>()
                        .Property(b => b.Id)
                        .HasColumnName("id");

            modelBuilder.Entity<OrderLineReadView>()
                       .Property(b => b.OrderId)
                       .HasColumnName("order_id");

            modelBuilder.Entity<OrderLineReadView>()
                       .Property(b => b.ProductId)
                       .HasColumnName("product_id");

            modelBuilder.Entity<OrderLineReadView>()
                       .Property(b => b.WarehouseId)
                       .HasColumnName("warehouse_id");

            modelBuilder.Entity<OrderLineReadView>()
                       .Property(b => b.Quantity)
                       .HasColumnName("quantity");

            modelBuilder.Entity<OrderLineReadView>()
                       .Property(b => b.Status)
                       .HasColumnName("status");
        }

        private static void BuildWarehouseEntities(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WarehouseReadView>()
                        .ToTable("warehouse_read_view");

            modelBuilder.Entity<WarehouseReadView>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<WarehouseReadView>()
                        .Property(b => b.Id)
                        .HasColumnName("id")
                        .ValueGeneratedNever();

            modelBuilder.Entity<WarehouseReadView>()
                        .Property(b => b.Name)
                        .HasColumnName("name");

            modelBuilder.Entity<WarehouseReadView>()
                        .HasMany(b => b.Items)
                        .WithOne(e => e.Warehouse)
                        .HasForeignKey(p => p.WarehouseId)
                        .IsRequired();

            modelBuilder.Entity<WarehouseItemReadView>()
                        .ToTable("warehouse_item_read_view");

            modelBuilder.Entity<WarehouseItemReadView>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<WarehouseItemReadView>()
                        .Property(b => b.Id)
                        .HasColumnName("id");

            modelBuilder.Entity<WarehouseItemReadView>()
                        .Property(b => b.ProductId)
                        .HasColumnName("product_id");

            modelBuilder.Entity<WarehouseItemReadView>()
                        .Property(b => b.Quantity)
                        .HasColumnName("quantity");

            modelBuilder.Entity<WarehouseItemReadView>()
                        .Property(b => b.WarehouseId)
                        .HasColumnName("warehouse_id");
        }
    }
}
