using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class ProductPart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MeasuringUnit { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public Product Product { get; set; }

        public Material Material { get; set; }
    }
}
