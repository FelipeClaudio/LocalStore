using LocalStore.Commons.Models;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;

namespace LocalStore.Domain.Services
{
    public interface IOrderService
    {
        IEnumerable<Product> GetTopNMostSoldProductsInDateRange(DateRange dateRange, int numberOfElement);
        decimal GetRevenueInDateRangeForProductId(DateRange dateRange, Guid id);
    }
}
