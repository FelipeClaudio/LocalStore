using LocalStore.Infrastructure.Database.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalStore.Infrastructure.Database.Orders
{
    public class OrderContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=BRRIOWN032980\SQLEXPRESS;Initial Catalog=LocalStore;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(o => o.Order)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderItem>()
                .HasKey(o => o.Id);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
