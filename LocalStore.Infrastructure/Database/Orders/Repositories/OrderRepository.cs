using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Infrastructure.Database.Orders.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Infrastructure.Database.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            this._context = context;
        }

        public void Delete(Guid id)
        {
            Order order = this.GetOrderById(id);
            this._context.Orders.Remove(order.ToRepositoryModel());
            this._context.SaveChanges();
        }

        public IList<Order> GetOrders()
        {
            return this._context.Orders
                       .Include(o => o.Items)
                       .ThenInclude(o => o.Product)
                       .ThenInclude(o => o.ProductParts)
                       .ThenInclude(o => o.Material)
                       .Select(o => o.ToDomainModel()).ToList();
        }

        public Order GetOrderById(Guid id)
        {
            return this._context.Orders.Where(o => o.Id == id).FirstOrDefault().ToDomainModel();
        }

        public IList<Order> GetOrdersInDateRange(DateTime startingDate, DateTime endDate)
        {
            return this._context
                    .Orders
                    .Where(o => o.OrderDate >= startingDate && o.OrderDate <= endDate)
                    .Select(o => o.ToDomainModel())
                    .ToList();
        }

        public void Insert(Order entity)
        {
            this._context.Orders.Add(entity.ToRepositoryModel());
            this._context.SaveChanges();
        }

        public ProductPart GetPart(Guid id)
        {
            return this._context.ProductParts.FirstOrDefault(p => p.Id == id).ToDomainModel();
        }

        public IList<ProductPart> GetParts()
        {
            return this._context.ProductParts.Select(p => p.ToDomainModel()).ToList();
        }

        public Product GetProduct(Guid id)
        {
            return this._context.Products.FirstOrDefault(p => p.Id == id).ToDomainModel();
        }

        public IList<Product> GetProducts()
        {
            return this._context.Products.Select(p => p.ToDomainModel()).ToList();
        }
    }
}
