using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;

namespace LocalStore.Application.Models
{
    public class OrderInfo
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderItemInfo> OrderItemInfos { get; set; }
    }

    public class OrderItemInfo
    {
        public Guid OrderItemId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnityPrice { get; set; }

        public Product Product { get; set; }
    }
}
