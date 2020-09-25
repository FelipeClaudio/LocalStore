using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalStore.Domain.Services
{
    public interface IOrderService
    {
        IEnumerable<Product> GetTopNMostSoldProductsInDateRange(DateRange dateRange, int numberOfElements);
        
        IEnumerable<Product> GetTopNLessSoldProductsInDateRange(DateRange dateRange, int numberOfElements);
        
        decimal GetRevenueInDateRangeForProductId(DateRange dateRange, Guid id);
        
        Task InsertOrder(Order order);
        
        IEnumerable<Order> GetAllOrdersForDateRange(DateRange dateRange);
    }
}
