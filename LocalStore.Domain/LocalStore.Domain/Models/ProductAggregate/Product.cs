using LocalStore.Commons.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Domain.Models.ProductAggregate
{
    public class Product : EntityBase
    {
        public Product(Guid? id = null) : base(id) { }

        [Required]
        public string Name { get; set; }

        public ICollection<ProductPart> ProductParts { get; set; }
    }
}
