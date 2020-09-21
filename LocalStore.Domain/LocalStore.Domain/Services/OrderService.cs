using LocalStore.Commons.Models;
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

        public IEnumerable<Product> GetTopNMostSoldProductsInDateRange(DateRange dateRange, int numberOfElements)
        {
            IEnumerable<OrderItem> orderItems = GetOrderItemsForDateRange(dateRange);
            IEnumerable<Product> productsForOrders = this._productRepository
                                                        .GetProducts()
                                                        .Join(orderItems, product => product.Id, item => item.ProductId,
                                                        (product, order) => product);

            var mostSoldProducts = productsForOrders
                                    .GroupBy(p => p.Name)
                                    .Select(p => new { Name = p.Key, Count = p.Count() })
                                    .OrderByDescending(p => p.Count)
                                    .Take(numberOfElements);



            return mostSoldProducts.Select(product => this._productRepository.GetProductByName(product.Name));
        }
        public decimal GetRevenueInDateRangeForProductId(DateRange dateRange, Guid id)
        {
            var orderItems = GetOrderItemsForDateRange(dateRange);

            return orderItems.Where(o => o.ProductId == id)
                             .Select(o => new { orderRevenue = o.Quantity * o.UnitPrice })
                             .Sum(o => o.orderRevenue);
        }

        private IEnumerable<OrderItem> GetOrderItemsForDateRange(DateRange dateRange)
        {
            ICollection<Order> ordersInDateRange = this._orderRepository.GetOrdersInDateRange(dateRange);
            IEnumerable<OrderItem> orderItems = ordersInDateRange.SelectMany(o => o.Items);

            return orderItems;
        }

    }
}
