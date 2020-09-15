using LocalStore.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        IList<Product> GetProducts();
        Product GetProductById(Guid id);
        Product GetProductByName(string name);
        IList<ProductPart> GetParts();
        ProductPart GetPartById(Guid id);
    }
}
