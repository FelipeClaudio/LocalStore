using LocalStore.Infrastructure.Database.Orders.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductParts)
                .WithOne(p => p.Product)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Material>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<ProductPart>()
                .HasKey(p => p.Id);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductPart> ProductParts { get; set; }
    }
}
