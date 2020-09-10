using LocalStore.Domain.Models.OrderAggregate;
using System.Linq;

namespace LocalStore.Infrastructure.Database.Orders.Mappers
{
    public static class ProductMapper
    {
        public static Product ToDomainModel(this Models.Product product)
        {
            return new Product(product.Id)
            {
                Name = product.Name,
                ProductParts = product.ProductParts.Select(p => p.ToDomainModel()).ToList()
            };
        }

        public static Models.Product ToRepositoryModel(this Product product)
        {
            return new Models.Product
            {
                Name = product.Name,
                ProductParts = product.ProductParts.Select(p => p.ToRepositoryModel()).ToList()
            };
        }
    }
}
