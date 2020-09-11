using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Infrastructure.Database.Orders;
using LocalStore.Infrastructure.Database.Orders.Repositories;
using LocalStore.Infrastructure.Database.Products;
using LocalStore.Infrastructure.Database.Products.Repositories;
using System;
using System.Collections.Generic;

namespace TestApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var productsRespository = new ProductRepository(new ProductContext());

            var partList = new List<ProductPart> {
                    new ProductPart("Some-Part", "grams", 1, new Material ("Some-Material1", "Some-Description1")),
                    new ProductPart("Some-Part", "grams", 1, new Material ("Some-Material2", "Some-Description2")),
            };

            var product1 = new Product
            {
                Name = "Some-Product1",
                ProductParts = partList
            };
            productsRespository.Insert(product1);

            var product2 = new Product
            {
                Name = "Some-Product2",
                ProductParts = partList
            };
            productsRespository.Insert(product2);

            var products = productsRespository.GetProducts();

            var ordersRepository = new OrderRepository(new OrderContext());
            var order1 = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product1.Id,
                        Quantity = 3,
                        UnitPrice = 5
                    }
                },
                OrderDate = DateTime.Now
            };
            ordersRepository.Insert(order1);

            var order2 = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product2.Id,
                        Quantity = 3,
                        UnitPrice = 5
                    }
                },
                OrderDate = DateTime.Now
            };
            ordersRepository.Insert(order2);

            var orders = ordersRepository.GetOrders();
        }
    }
}
