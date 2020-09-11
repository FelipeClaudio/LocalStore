using LocalStore.Domain.Models.ProductAggregate;
using System;

namespace LocalStore.Domain.Services
{
    public interface IOrderService
    {
        Product GetMostSoldProductInDateRange(DateTime initialDate, DateTime finalDate);
    }
}
