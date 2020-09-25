using System;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class ProductPartMaterial : DatabaseEntityBase
    {
        public Guid ProductPartId { get; set; }
        public ProductPart ProductPart { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
