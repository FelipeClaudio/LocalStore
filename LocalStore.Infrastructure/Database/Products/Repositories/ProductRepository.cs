﻿using Force.DeepCloner;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Infrastructure.Database.Products.Mappers;
using LocalStore.Infrastructure.Database.Products.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalStore.Infrastructure.Database.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            this._context = context;
        }

        public Domain.Models.ProductAggregate.ProductPart GetPartById(Guid id)
        {
            return this._context.ProductParts
                        .Include(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Id == id)
                        .ToDomainModel();
        }

        public IList<Domain.Models.ProductAggregate.ProductPart> GetParts()
        {
            return this._context.ProductParts
                        .Include(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .Select(p => p.ToDomainModel())
                        .ToList();
        }

        public Domain.Models.ProductAggregate.Product GetProductById(Guid id)
        {
            return this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Id == id)
                        .ToDomainModel();
        }

        public Domain.Models.ProductAggregate.Product GetProductByName(string name)
        {
            return this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Name == name)
                        .ToDomainModel();
        }

        public IList<Domain.Models.ProductAggregate.Product> GetProducts()
        {
            return this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .Select(p => p.ToDomainModel())
                        .ToList();
        }

        public void Insert(Domain.Models.ProductAggregate.Product entity)
        {
            Models.Product product = entity.ToRepositoryModel();

            // avoids inserting children elements
            var productClone = product.DeepClone();
            productClone.ProductParts = null;
            
            this.InsertProduct(productClone);

            IEnumerable<Models.Material> materials = product.ProductParts
                .Select(p => p.ProductPartMaterials.Select(m => m.Material))
                .SelectMany(m => m);

            IEnumerable<Models.Material> insertedMaterials = this.InsertMaterials(materials);

            this.InsertProductParts(product.ProductParts, product, insertedMaterials);
        }

        private void InsertProduct(Models.Product product)
        {
            if (!ProductExists(product))
            {
                this._context.Products.Add(product);
            }
        }

        private bool ProductExists(Models.Product product)
        {
            return this._context.Products.Any(p => p.Name == product.Name);
        }

        private void InsertProductParts(ICollection<Models.ProductPart> productParts, Models.Product product, IEnumerable<Models.Material> insertedMaterials)
        {
            foreach (Models.ProductPart productPart in productParts)
            {
                if (!this.ProductPartExists(productPart))
                {
                    var materials = productPart.ProductPartMaterials.Join
                        (
                            insertedMaterials,
                            productPartMaterial => productPartMaterial.Material.Name,
                            material => material.Name,
                            (productPartMaterial, material) => material
                        );

                    var productPartMaterials = materials.Select(material =>
                        new ProductPartMaterial
                        {
                            ProductPart = productPart,
                            Material = material,
                            ProductPartId = productPart.Id,
                            CreationTime = DateTime.Now,
                        }).ToList();

                  this._context.ProductPartMaterials.AddRange(productPartMaterials);

                    productPart.ProductPartMaterials = productPartMaterials.ToList();
                    productPart.ProductId = product.Id;

                    this._context.ProductParts.Add(productPart);
                }
            }
        }

        private bool ProductPartExists(Models.ProductPart productPart)
        {
            return this._context.ProductParts
                       .Include(p => p.ProductPartMaterials)
                       .ThenInclude(p => p.Material)
                       .Any(p =>
                            p.Name == productPart.Name &&
                            p.Quantity == productPart.Quantity &&
                            p.MeasuringUnit == productPart.MeasuringUnit &&
                            p.ExpirationDate == null);

            // TODO: Validate product name.
        }

        private IEnumerable<Models.Material> InsertMaterials(IEnumerable<Models.Material> materials)
        {
            IEnumerable<Models.Material> notExistentMaterial = materials.Where(material => !MaterialExistsAndIsValid(material));

            this._context.Materials.AddRange(notExistentMaterial);

            return notExistentMaterial;
        }

        private bool MaterialExistsAndIsValid(Models.Material material)
        {
            return this._context.Materials.Any(m =>
                m.Name == material.Name &&
                m.ExpirationDate == null);
        }

        public void DeleteById(Guid id)
        {
            Models.Product product = this._context.Products.FirstOrDefault(p => p.Id == id);
            this._context.Products.Remove(product);
        }

        public Task SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}
