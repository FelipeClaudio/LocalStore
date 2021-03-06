﻿using LocalStore.Commons.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public class Order : EntityBase, IAggregateRoot
    {
        public Order(Guid? id = null) : base(id) { }

        [Required]
        public List<OrderItem> Items { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
