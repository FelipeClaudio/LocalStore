using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Infrastructure.Database.Products.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Infrastructure.Database.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            this._context = context;
        }

        public ProductPart GetPart(Guid id)
        {
            return  this._context.ProductParts
                        .Include(p => p.Material)
                        .FirstOrDefault(p => p.Id == id)
                        .ToDomainModel();
        }

        public IList<ProductPart> GetParts()
        {
            return  this._context.ProductParts
                        .Include(p => p.Material)
                        .Select(p => p.ToDomainModel())
                        .ToList();
        }

        public Product GetProduct(Guid id)
        {
            return  this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Id == id).ToDomainModel();
        }

        public IList<Product> GetProducts()
        {
            return  this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.Material)
                        .Select(p => p.ToDomainModel()).ToList();
        }

        public void Insert(Product entity)
        {
            this._context.Products.Add(entity.ToRepositoryModel());
            this._context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Models.Product product = this._context.Products.FirstOrDefault(p => p.Id == id);
            this._context.Products.Remove(product);
        }
    }
}
