using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using LocalStore.Infrastructure.Database.Orders;
using LocalStore.Infrastructure.Database.Orders.Repositories;
using LocalStore.Infrastructure.Database.Products;
using LocalStore.Infrastructure.Database.Products.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocalStore.Application
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string ordersDbConnectionString = Configuration.GetConnectionString("OrdersDb");
            string productsDbConnectionString = Configuration.GetConnectionString("ProductsDb");

            services.AddControllers();
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(ordersDbConnectionString));
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(productsDbConnectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Local Store V1");
            });
        }
    }
}
