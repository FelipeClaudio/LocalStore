using LocalStore.Domain.Models.ProductAggregate;

namespace LocalStore.Infrastructure.Database.Products.Mappers
{
    public static class ProductPartMapper
    {
        public static ProductPart ToDomainModel(this Models.ProductPart productPart)
        {
            return new ProductPart(productPart.Name, productPart.MeasuringUnit, productPart.Quantity, new Material(productPart.Material.Name, productPart.Material.Description));
        }

        public static Models.ProductPart ToRepositoryModel(this ProductPart productPart)
        {
            return new Models.ProductPart
            {
                MeasuringUnit = productPart.MeasuringUnit,
                Name = productPart.Name,
                Material = new Models.Material()
                {
                    Name = productPart.Material.Name,
                    Description = productPart.Material.Description
                }
            };
        }
    }
}
