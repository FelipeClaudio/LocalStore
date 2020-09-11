using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Domain.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
        }

        public Product GetMostSoldProductInDateRange(DateTime initialDate, DateTime finalDate)
        {
            ICollection<Order> ordersInDateRange = this._orderRepository.GetOrdersInDateRange(initialDate, finalDate);
            var mostSoldProduct = ordersInDateRange
                                    .GroupBy(o => o.Id)
                                    .Select(x => new { Id = x.Key, Count = x.Count() })
                                    .OrderBy(x => x.Count)
                                    .First();

            return this._productRepository.GetProduct(mostSoldProduct.Id);
        }
    }
}
