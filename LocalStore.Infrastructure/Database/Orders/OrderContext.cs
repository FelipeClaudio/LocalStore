using LocalStore.Infrastructure.Database.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalStore.Infrastructure.Database.Orders
{
    public class OrderContext : DbContext
    {
        public OrderContext() { }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationUtilities.GetConnectionStringFromConnectionKey("OrdersDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(o => o.Order)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<OrderItem>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderItem>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
