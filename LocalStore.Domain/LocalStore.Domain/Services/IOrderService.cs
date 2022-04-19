using LocalStore.Commons.Models;
using LocalStore.Domain.Models.Order;
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

        IEnumerable<OrderAdditionalInfo> GetRevenuesInDateRangeForProductId(DateRange dateRange);
        
        Task InsertOrder(Order order);
        
        IEnumerable<Order> GetAllOrdersInDateRange(DateRange dateRange);
    }
}
