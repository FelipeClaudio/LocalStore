using LocalStore.Domain.Models.ProductAggregate;

namespace LocalStore.Infrastructure.Database.Products.Mappers
{
    public static class MaterialMapper
    {
        public static Material ToDomainModel(this Models.Material product)
        {
            return new Material(product.Name, product.Description, product.Price);
        }

        public static Models.Material ToRepositoryModel(this Material product)
        {
            return new Models.Material()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}
