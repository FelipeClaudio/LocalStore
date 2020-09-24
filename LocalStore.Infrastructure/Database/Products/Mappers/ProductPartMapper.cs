using LocalStore.Infrastructure.Database.Products.Models;
using System.Linq;
using ProductPart = LocalStore.Domain.Models.ProductAggregate.ProductPart;

namespace LocalStore.Infrastructure.Database.Products.Mappers
{
    public static class ProductPartMapper
    {
        public static ProductPart ToDomainModel(this Models.ProductPart productPart)
        {
            return new ProductPart
            (
                productPart.Name,
                productPart.MeasuringUnit,
                productPart.Quantity,
                productPart.ProductPartMaterials
                    .Select(p => new Domain.Models.ProductAggregate.Material(
                                    p.Material.Name,
                                    p.Material.Description,
                                    p.Material.Price)).ToList()
            );
        }

        public static Models.ProductPart ToRepositoryModel(this ProductPart productPart)
        {
            return new Models.ProductPart
            {
                MeasuringUnit = productPart.MeasuringUnit,
                Name = productPart.Name,
                ProductPartMaterials = productPart
                    .Materials
                    .Select(material => new ProductPartMaterial { Material = material.ToRepositoryModel() })
                    .ToList()
            };
        }
    }
}
