using LocalStore.Domain.Models.OrderAggregate;

namespace LocalStore.Infrastructure.Database.Orders.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItem ToDomainModel(this Models.OrderItem orderItem) 
        {
            return new OrderItem(orderItem.Id)
            {
                Product = orderItem.Product.ToDomainModel(),
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }

        public static Models.OrderItem ToRepositoryModel(this OrderItem orderItem)
        {
            return new Models.OrderItem
            {
                Id = orderItem.Id,
                Product = orderItem.Product.ToRepositoryModel(),
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice
            };
        }
    }
}
