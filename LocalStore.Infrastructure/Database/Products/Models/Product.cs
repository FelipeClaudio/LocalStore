using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class Product : DatabaseEntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public virtual ICollection<ProductPart> ProductParts { get; set; }
    }
}
