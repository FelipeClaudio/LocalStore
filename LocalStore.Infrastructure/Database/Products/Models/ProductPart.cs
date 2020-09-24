using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class ProductPart : DatabaseEntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MeasuringUnit { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public ICollection<ProductPartMaterial> ProductPartMaterials { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
