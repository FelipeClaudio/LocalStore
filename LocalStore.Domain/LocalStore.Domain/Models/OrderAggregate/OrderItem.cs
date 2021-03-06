﻿using LocalStore.Commons.Definitions;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public class OrderItem : EntityBase
    {
        public OrderItem(Guid? id = null) : base(id) { }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // TODO: Add extra costs example: delivering, stocking etc.
    }
}
