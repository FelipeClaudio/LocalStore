using LocalStore.Domain.Models.OrderAggregate;

namespace LocalStore.Infrastructure.Database.Orders.Mappers
{
    public static class MaterialMapper
    {
        public static Material ToDomainModel(this Models.Material material)
        {
            return new Material
            {
                Name = material.Name
            };
        }

        public static Models.Material ToRepositoryModel(this Material material)
        {
            return new Models.Material
            {
                Name = material.Name
            };
        }
    }
}
