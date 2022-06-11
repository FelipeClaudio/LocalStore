using LocalStore.Infrastructure.Database.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalStore.Infrastructure.Database.Products
{
    public class ProductContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationUtilities.GetConnectionStringFromConnectionKey("ProductsDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ProductPart>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductPart>()
                .Property(b => b.CreationTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ProductPartMaterial>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProductPartMaterial>()
                .HasOne(p => p.Material)
                .WithMany(p => p.ProductPartMaterials)
                .HasForeignKey(p => p.MaterialId);

            modelBuilder.Entity<ProductPartMaterial>()
                .HasOne(p => p.ProductPart)
                .WithMany(p => p.ProductPartMaterials)
                .HasForeignKey(p => p.ProductPartId);

            modelBuilder.Entity<ProductPartMaterial>()
                .Property(m => m.CreationTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Material>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Material>()
                .Property(m => m.CreationTime)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPart> ProductParts { get; set; }
        public DbSet<ProductPartMaterial> ProductPartMaterials { get; set; }
        public DbSet<Material> Materials { get; set; }
    }
}
