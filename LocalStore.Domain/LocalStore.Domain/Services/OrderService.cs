using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Domain.Services
{
    public class OrderService : IOrderService
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
            IEnumerable<OrderItem> orderItems = ordersInDateRange.SelectMany(o => o.Items);
            IEnumerable<Product> productsForOrders = this._productRepository
                                                        .GetProducts()
                                                        .Join(orderItems, product => product.Id, item => item.ProductId, 
                                                        (product, order) => product);

            var mostSoldProduct = productsForOrders
                                    .GroupBy(p => p.Name)
                                    .Select(p => new { Name = p.Key, Count = p.Count() })
                                    .OrderByDescending(p => p.Count)
                                    .FirstOrDefault();

            

            return this._productRepository.GetProductByName(mostSoldProduct.Name);
        }
    }
}
