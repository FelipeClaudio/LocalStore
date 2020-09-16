using LocalStore.Commons.Models;
using LocalStore.Domain.Models.ProductAggregate;
using System;

namespace LocalStore.Domain.Services
{
    public interface IOrderService
    {
        Product GetMostSoldProductInDateRange(DateRange dateRange);
        decimal GetRevenueInDateRangeForProductId(DateRange dateRange, Guid id);
    }
}
