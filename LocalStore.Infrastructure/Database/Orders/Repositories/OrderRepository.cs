using AutoMapper;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
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
        private readonly IMapper _mapper;

        public OrderRepository(OrderContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public void DeleteById(Guid id)
        {
            Order order = this.GetOrderById(id);
            this._context.Orders.Remove(this._mapper.Map<Order, Models.Order>(order));
        }

        public IList<Order> GetOrders()
        {
            return this._context.Orders
                .Include(order => order.Items)
                .Select(order => this._mapper.Map<Models.Order, Order>(order))
                .ToList();
        }

        public Order GetOrderById(Guid id)
        {
            return this._mapper.Map<Models.Order, Order>(this._context.Orders.Where(o => o.Id == id).FirstOrDefault());
        }

        public IList<Order> GetOrdersInDateRange(DateRange dateRange)
        {
            return  this._context.Orders
                        .Include(order => order.Items)
                        .Where(order => order.OrderDate >= dateRange.InitialDate && order.OrderDate <= dateRange.FinalDate)
                        .Select(order => this._mapper.Map<Models.Order, Order>(order))
                        .ToList();
        }

        public void Insert(Order entity)
        {
            this._context.Orders.Add(this._mapper.Map<Order, Models.Order>(entity));
            this._context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}
