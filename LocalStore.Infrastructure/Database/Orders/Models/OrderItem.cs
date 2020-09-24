using System;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Infrastructure.Database.Orders.Models
{
    public class OrderItem : DatabaseEntityBase
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
