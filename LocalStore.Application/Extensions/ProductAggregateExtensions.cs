using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using LocalStore.Infrastructure.Database.Products;
using LocalStore.Infrastructure.Database.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalStore.Application.Extensions
{
    public static class ProductAggregateExtensions
    {
        public static IServiceCollection AddProductAggregate(this IServiceCollection services, IConfiguration configuration)
        {
            string productsDbConnectionString = configuration.GetConnectionString("ProductsDb");

            services.AddDbContext<ProductContext>(options => options.UseSqlServer(productsDbConnectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
