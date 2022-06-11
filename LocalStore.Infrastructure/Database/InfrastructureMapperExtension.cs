using Microsoft.Extensions.DependencyInjection;

namespace LocalStore.Infrastructure.Database
{
    public static class InfrastructureMapperExtension
    {
        public static void AddInfrastuctureMapperExtension(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
