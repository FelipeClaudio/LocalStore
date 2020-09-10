using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Infrastructure.Database.Orders;
using LocalStore.Infrastructure.Database.Orders.Repositories;
using System;
using System.Collections.Generic;

namespace TestApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ordersRespository = new OrderRepository(new OrderContext());

            var domainProduct = new Product
            {
                Name = "Some-Product",
                ProductParts = new List<ProductPart> {
                    new ProductPart("Some-Part", "grams", 1, new Material { Name = "Some-Material" })
                }
            };

            var domainOrder = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Product = domainProduct,
                        Quantity = 3,
                        UnitPrice = 5
                    }
                },
                OrderDate = DateTime.Now
            };

            ordersRespository.Insert(domainOrder);
            var order = ordersRespository.GetOrders();
            var x = 1;
        }
    }
}
