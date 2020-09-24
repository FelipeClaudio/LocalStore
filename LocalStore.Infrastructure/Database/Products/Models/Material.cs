using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class Material : DatabaseEntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<ProductPartMaterial> ProductPartMaterials { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
