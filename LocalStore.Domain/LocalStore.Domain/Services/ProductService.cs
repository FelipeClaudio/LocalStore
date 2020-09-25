using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalStore.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this._productRepository.GetProducts();
        }

        public Product GetProductById(Guid id)
        {
            return this._productRepository.GetProductById(id);
        }

        public async Task InsertProduct(Product product)
        {
            this._productRepository.Insert(product);
            await this._productRepository.SaveChangesAsync();
        }
    }
}
