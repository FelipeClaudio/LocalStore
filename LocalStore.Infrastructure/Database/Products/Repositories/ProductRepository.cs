using AutoMapper;
using Force.DeepCloner;
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
        private readonly IMapper _mapper;

        public ProductRepository(ProductContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Domain.Models.ProductAggregate.ProductPart GetPartById(Guid id)
        {
            var productPart = this._context.ProductParts
                        .Include(productPart => productPart.ProductPartMaterials)
                        .ThenInclude(material => material.Material)
                        .FirstOrDefault(product => product.Id == id);

            return this._mapper.Map<Models.ProductPart, Domain.Models.ProductAggregate.ProductPart>(productPart);
        }

        public IList<Domain.Models.ProductAggregate.ProductPart> GetParts()
        {
            return this._context.ProductParts
                        .Include(productPart => productPart.ProductPartMaterials)
                        .ThenInclude(material => material.Material)
                        .Select(p => this._mapper.Map<Models.ProductPart, Domain.Models.ProductAggregate.ProductPart>(p))
                        .ToList();
        }

        public Domain.Models.ProductAggregate.Product GetProductById(Guid id)
        {
            var product = this._context.Products
                        .Include(productPart => productPart.ProductParts)
                        .ThenInclude(productPart => productPart.ProductPartMaterials)
                        .ThenInclude(material => material.Material)
                        .FirstOrDefault(product => product.Id == id);

            return this._mapper.Map<Models.Product, Domain.Models.ProductAggregate.Product>(product);
        }

        public Domain.Models.ProductAggregate.Product GetProductByName(string name)
        {
            var product = this._context.Products
                        .Include(p => p.ProductParts)
                        .ThenInclude(p => p.ProductPartMaterials)
                        .ThenInclude(p => p.Material)
                        .FirstOrDefault(p => p.Name == name);

            return this._mapper.Map<Models.Product, Domain.Models.ProductAggregate.Product>(product);
        }

        public IList<Domain.Models.ProductAggregate.Product> GetProducts()
        {
            return this._context.Products
                        .Include(product => product.ProductParts)
                        .ThenInclude(productPart => productPart.ProductPartMaterials)
                        .ThenInclude(productPartMaterial => productPartMaterial.Material)
                        .Select(product => this._mapper.Map<Models.Product, Domain.Models.ProductAggregate.Product>(product))
                        .ToList();
        }

        public void Insert(Domain.Models.ProductAggregate.Product entity)
        {
            Models.Product product = this._mapper.Map<Domain.Models.ProductAggregate.Product, Models.Product>(entity);

            // avoids inserting children elements
            var productClone = product.DeepClone();
            productClone.ProductParts = null;

            this.InsertProduct(productClone);

            IEnumerable<Models.Material> materials = product.ProductParts
                .Select(productPart => productPart.ProductPartMaterials.Select(m => m.Material))
                .SelectMany(material => material);

            this.InsertMaterials(materials);

            this.InsertProductParts(product);

        }

        private bool InsertProduct(Models.Product product)
        {
            if (!ProductExists(product))
            {
                this._context.Products.Add(product);
                return true;
            }
            return false;
        }

        private bool ProductExists(Models.Product product)
        {
            return this._context.Products.Any(p => p.Name == product.Name);
        }

        private void InsertProductParts(Models.Product product)
        {
            foreach (Models.ProductPart productPart in product.ProductParts)
            {
                var materials = productPart.ProductPartMaterials.Join
                    (
                        this._context.Materials,
                        productPartMaterial => productPartMaterial.Material.Name,
                        material => material.Name,
                        (productPartMaterial, material) => material
                    );

                var productPartMaterials = materials.Select(material =>
                    new ProductPartMaterial
                    {
                        MaterialId = material.Id,
                        ProductPartId = productPart.Id,
                        CreationTime = DateTime.Now,
                    }).ToList();

                this._context.ProductPartMaterials.AddRange(productPartMaterials);

                productPart.ProductPartMaterials = productPartMaterials.ToList();
                productPart.ProductId = product.Id;
                this._context.Entry(productPart).State = EntityState.Detached;

                this._context.ProductParts.Add(productPart);
            }
        }

        private IEnumerable<Models.Material> InsertMaterials(IEnumerable<Models.Material> materials)
        {
            IEnumerable<Models.Material> notExistentMaterials = materials.Where(material => !MaterialExistsAndIsValid(material))
                                                                         .GroupBy(material => material.Name)
                                                                         .Select(material => material.FirstOrDefault());

            this._context.Materials.AddRange(notExistentMaterials);

            // TODO: Remove this save changes.
            // This line simplifies ProductPartMaterials object assembly 
            // because it is only necessary to look for materials in one place (database),
            // instead of combining database and memory search.
            this._context.SaveChanges();

            return notExistentMaterials;
        }

        private bool MaterialExistsAndIsValid(Models.Material material)
        {
            return this._context.Materials.Any(currentMaterial =>
                currentMaterial.Name == material.Name &&
                currentMaterial.ExpirationDate == null);
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
