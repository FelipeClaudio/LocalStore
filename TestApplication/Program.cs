using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Infrastructure.Database.Orders;
using LocalStore.Infrastructure.Database.Orders.Repositories;
using LocalStore.Infrastructure.Database.Products;
using LocalStore.Infrastructure.Database.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TestApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connectionString = @"Data Source=BRRIOWN032980\SQLEXPRESS;Initial Catalog=LocalStore;Integrated Security=True";
            var productsOptionsBuilder = new DbContextOptionsBuilder();
            productsOptionsBuilder.UseSqlServer(connectionString);

            var productsRespository = new ProductRepository(new ProductContext());

            var material1 = new Material("Some-Material1", "Some-Description1", 2.50M);
            var material2 = new Material("Some-Material2", "Some-Description2", 4.75M);
            var material3 = new Material("Some-Material3", "Some-Description3", 7.25M);

            var partList = new List<ProductPart>
            {
                new ProductPart("Some-Part1", "grams", 1, new List<Material> {material1, material3}),
                new ProductPart("Some-Part2", "grams", 1, new List<Material> {material2, material3}),
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

            var order3 = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = product2.Id,
                        Quantity = 9,
                        UnitPrice = 2
                    }
                },
                OrderDate = DateTime.Now
            };
            ordersRepository.Insert(order3);

            var orders = ordersRepository.GetOrders();
        }
    }
}
