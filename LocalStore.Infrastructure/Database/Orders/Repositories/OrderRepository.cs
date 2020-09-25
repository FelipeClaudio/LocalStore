using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Infrastructure.Database.Orders.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalStore.Infrastructure.Database.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            this._context = context;
        }

        public void DeleteById(Guid id)
        {
            Order order = this.GetOrderById(id);
            this._context.Orders.Remove(order.ToRepositoryModel());
        }

        public IList<Order> GetOrders()
        {
            return this._context.Orders
                       .Include(o => o.Items)
                       .Select(o => o.ToDomainModel()).ToList();
        }

        public Order GetOrderById(Guid id)
        {
            return this._context.Orders.Where(o => o.Id == id).FirstOrDefault().ToDomainModel();
        }

        public IList<Order> GetOrdersInDateRange(DateRange dateRange)
        {
            return  this._context.Orders
                        .Include(o => o.Items)
                        .Where(o => o.OrderDate >= dateRange.InitialDate && o.OrderDate <= dateRange.FinalDate)
                        .Select(o => o.ToDomainModel())
                        .ToList();
        }

        public void Insert(Order entity)
        {
            this._context.Orders.Add(entity.ToRepositoryModel());
            this._context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}
