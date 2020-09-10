using System;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Orders.Models
{
    public class Material
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
