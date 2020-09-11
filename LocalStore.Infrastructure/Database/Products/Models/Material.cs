using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Products.Models
{
    public class Material
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
