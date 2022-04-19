using System;

namespace LocalStore.Domain.Models.Order
{
    public class OrderAdditionalInfo
    {
        public Guid ProductId { get; set; }
        public decimal Revenue { get; set; }
        public decimal Quantity { get; set; }
    }
}
