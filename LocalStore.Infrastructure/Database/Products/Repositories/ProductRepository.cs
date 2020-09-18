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

        public ProductPart GetPartById(Guid id)
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

        public Product GetProductById(Guid id)
        {
            return  this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Id == id)
                        .ToDomainModel();
        }

        public Product GetProductByName(string name)
        {
            return this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Name == name)
                        .ToDomainModel();
        }

        public IList<Product> GetProducts()
        {
            return  this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.Material)
                        .Select(p => p.ToDomainModel())
                        .ToList();
        }

        public void Insert(Product entity)
        {
            if (this._context.Products.FirstOrDefault(p => p.Name.Equals(entity.Name)) == null)
            {
                this._context.Products.Add(entity.ToRepositoryModel());
                this._context.SaveChanges();
            }
        }

        public void DeleteById(Guid id)
        {
            Models.Product product = this._context.Products.FirstOrDefault(p => p.Id == id);
            this._context.Products.Remove(product);
        }
    }
}
