using LocalStore.Commons.Models;
using LocalStore.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        IList<Order> GetOrders();
        IList<Order> GetOrdersInDateRange(DateRange dateRange);
        Order GetOrderById(Guid id);
    }
}
