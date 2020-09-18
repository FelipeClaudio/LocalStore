using LocalStore.Infrastructure.Database.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalStore.Infrastructure.Database.Products
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public ProductContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=BRRIOWN032980\SQLEXPRESS;Initial Catalog=LocalStore;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductParts)
                .WithOne(p => p.Product)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ProductPart>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductPart>()
                .OwnsOne(p => p.Material);

            modelBuilder.Entity<ProductPart>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPart> ProductParts { get; set; }
    }
}
