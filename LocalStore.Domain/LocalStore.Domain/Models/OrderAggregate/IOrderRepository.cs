using LocalStore.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace LocalStore.Domain.Models.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        IList<Order> GetOrders();
        IList<Order> GetOrdersInDateRange(DateTime startingDate, DateTime endDate);
        Order GetOrderById(Guid id);
        IList<Product> GetProducts();
        Product GetProduct(Guid id);
        IList<ProductPart> GetParts();
        ProductPart GetPart(Guid id);
    }
}
