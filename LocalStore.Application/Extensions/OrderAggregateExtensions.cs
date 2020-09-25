using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Services;
using LocalStore.Infrastructure.Database.Orders;
using LocalStore.Infrastructure.Database.Orders.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalStore.Application.Extensions
{
    public static class OrderAggregateExtensions
    {
        public static IServiceCollection AddOrderAggregate(this IServiceCollection services, IConfiguration configuration)
        {
            string ordersDbConnectionString = configuration.GetConnectionString("OrdersDb");

            services.AddDbContext<OrderContext>(options => options.UseSqlServer(ordersDbConnectionString));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
