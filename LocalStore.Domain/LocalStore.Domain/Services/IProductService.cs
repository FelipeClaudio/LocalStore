using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalStore.Domain.Services
{
    public interface IProductService
    {
        public IEnumerable<Product> GetAllProducts();
        
        public Task InsertProduct(Product product);
        
        public Product GetProductById(Guid id);
    }
}
