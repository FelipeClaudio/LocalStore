using LocalStore.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        IList<Product> GetProducts();
        Product GetProduct(Guid id);
        IList<ProductPart> GetParts();
        ProductPart GetPart(Guid id);
    }
}
