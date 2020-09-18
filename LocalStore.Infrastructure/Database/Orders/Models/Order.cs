using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Orders.Models
{
    public class Order : DatabaseEntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual ICollection<OrderItem> Items { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
