using LocalStore.Commons;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public class OrderItem : EntityBase
    {
        public OrderItem(Guid? id = null) : base(id) { }

        [Required]
        public Product Product { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
