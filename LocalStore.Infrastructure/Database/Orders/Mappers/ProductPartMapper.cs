using LocalStore.Domain.Models.OrderAggregate;

namespace LocalStore.Infrastructure.Database.Orders.Mappers
{
    public static class ProductPartMapper
    {
        public static ProductPart ToDomainModel(this Models.ProductPart productPart)
        {
            return new ProductPart(productPart.Name, productPart.MeasuringUnit, productPart.Quantity, new Material { Name = productPart.Name });
        }

        public static Models.ProductPart ToRepositoryModel(this ProductPart productPart)
        {
            return new Models.ProductPart
            {
                MeasuringUnit = productPart.MeasuringUnit,
                Name = productPart.Name,
                Material = productPart.Material.ToRepositoryModel()
            };
        }
    }
}
