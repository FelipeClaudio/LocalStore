using LocalStore.Domain.Models.OrderAggregate;
using System.Linq;

namespace LocalStore.Infrastructure.Database.Orders.Mappers
{
    public static class OrderMapper
    {
        public static Order ToDomainModel(this Models.Order order)
        {
            return new Order(order.Id)
            {
                Items = order.Items.Select(i => i.ToDomainModel()).ToList(),
                OrderDate = order.OrderDate,
            };
        }

        public static Models.Order ToRepositoryModel(this Order order)
        {
            return new Models.Order
            {
                Id = order.Id,
                Items = order.Items.Select(i => i.ToRepositoryModel()).ToList(),
                OrderDate = order.OrderDate
            };
        }
    }
}
