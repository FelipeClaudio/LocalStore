using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public decimal GetRevenueInDateRangeForProductId(DateRange dateRange, Guid id)
        {
            var orderItems = GetOrderItemsForDateRange(dateRange);

            return orderItems.Where(o => o.ProductId == id)
                             .Select(o => new { orderRevenue = o.Quantity * o.UnitPrice })
                             .Sum(o => o.orderRevenue);
        }

        public IEnumerable<Product> GetTopNMostSoldProductsInDateRange(DateRange dateRange, int numberOfElements)
        {
            IEnumerable<OrderItem> orderItems = GetOrderItemsForDateRange(dateRange);
            var ordersCountByProductId = orderItems.GroupBy(o => o.ProductId)
                                                   .Select(o => new { ProductId = o.Key, Count = o.Sum(oi => oi.Quantity) })
                                                   .OrderByDescending(o => o.Count)
                                                   .Take(numberOfElements);

            return ordersCountByProductId.Select(product => this._productRepository.GetProductById(product.ProductId));
        }

        public IEnumerable<Product> GetTopNLessSoldProductsInDateRange(DateRange dateRange, int numberOfElements)
        {
            IEnumerable<OrderItem> orderItems = GetOrderItemsForDateRange(dateRange);
            var ordersCountByProductId = orderItems.GroupBy(o => o.ProductId)
                                                   .Select(o => new { ProductId = o.Key, Count = o.Sum(oi => oi.Quantity) })
                                                   .OrderBy(o => o.Count)
                                                   .Take(numberOfElements);

            return ordersCountByProductId.Select(product => this._productRepository.GetProductById(product.ProductId));
        }

        private IEnumerable<OrderItem> GetOrderItemsForDateRange(DateRange dateRange)
        {
            ICollection<Order> ordersInDateRange = this._orderRepository.GetOrdersInDateRange(dateRange);
            IEnumerable<OrderItem> orderItems = ordersInDateRange.SelectMany(o => o.Items);

            return orderItems;
        }

        public async Task InsertOrder(Order order)
        {
            IEnumerable<Guid> productIds = order.Items.Select(i => i.ProductId);
            if (AllProductsExist(productIds))
            {
                // Sets order date if not previously set.
                order.OrderDate = order.OrderDate != default ? order.OrderDate : DateTime.Now;
                
                this._orderRepository.Insert(order);

                await this._orderRepository.SaveChangesAsync();
            }
        }

        private bool AllProductsExist(IEnumerable<Guid> productIds)
        {
            return this._productRepository
                        .GetProducts()
                        .Select(p => p.Id)
                        .Intersect(productIds)
                        .Count() == productIds.Count();
        }

        public IEnumerable<Order> GetAllOrdersInDateRange(DateRange dateRange)
        {
            return this._orderRepository.GetOrdersInDateRange(dateRange);
        }
    }
}
