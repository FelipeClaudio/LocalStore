using Force.DeepCloner;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using System;
using System.Collections.Generic;

namespace LocalStore.UnitTests.Helpers
{
    public static class StubGenerator
    {
        private static readonly List<Guid> guidListStub = new List<Guid>
        {
            new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0191"),
            new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0192"),
            new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0193")
        };

        private static readonly List<Product> productListStub = new List<Product>
        {
                new Product(StubGenerator.guidListStub[0])
                {
                    Name = "boring product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("tire", "grams", 5 , new List<Material> { new Material("latex", "malleable material", 1M) }),
                        new ProductPart ("battery", "grams", 1 ,  new List<Material> { new Material("copper", "electronegative element", 2.5M) })
                    }
                },
                new Product(StubGenerator.guidListStub[1])
                {
                    Name = "ultra cool product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("button", "grams", 10 , new List<Material>
                        {
                            new Material("adamantium", "world's tougher material", 3.2M),
                            new Material("uranium", "radioactive material", 1.5M)
                        }),
                        new ProductPart ("power switch", "grams", 5 , new List<Material> { new Material("carbon", "a nice conductor", 2M) })
                    }
                },
                new Product(StubGenerator.guidListStub[2])
                {
                    Name = "new product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("window", "grams", 300 , new List<Material> { new Material("glass", "Extremely fragile", 4.2M) })
                    }
                }
        };

        private static readonly List<Order> orderListStub = new List<Order>
        {
            new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = StubGenerator.guidListStub[2],
                        Quantity = 3,
                        UnitPrice = 5
                    },
                    new OrderItem
                    {
                        ProductId = StubGenerator.guidListStub[1],
                        Quantity = 10,
                        UnitPrice = 2
                    },
                    new OrderItem
                    {
                        ProductId = StubGenerator.guidListStub[0],
                        Quantity = 1,
                        UnitPrice = 30
                    },
                    new OrderItem
                    {
                        ProductId = StubGenerator.guidListStub[2],
                        Quantity = 2,
                        UnitPrice = 60
                    }
                },
                OrderDate = new DateTime(2020, 3, 12)
            }
        };

        public static List<Guid> GetGuidListStub()
        {
            return StubGenerator.guidListStub.DeepClone();
        }

        public static List<Product> GetProductsListStub()
        {
            return StubGenerator.productListStub.DeepClone();
        }

        public static List<Order> GetOrdersListStub()
        {
            return StubGenerator.orderListStub.DeepClone();
        }
    }
}
