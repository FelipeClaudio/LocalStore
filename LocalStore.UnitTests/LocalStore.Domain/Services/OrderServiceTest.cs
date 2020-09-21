using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Xunit;
using Moq;
using System.Collections.Generic;
using System;
using FluentAssertions;
using LocalStore.Commons.Models;
using System.Linq;

namespace LocalStore.UnitTests.LocalStore.Domain.Services
{
    public class OrderServiceTest
    {
        private List<Order> _ordersInDateRangeStub;
        private List<Product> _getProductsListStub;

        private readonly Mock<IOrderRepository> _orderRespositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        private readonly List<Guid> _guidList;
        
        private readonly OrderService _orderService;

        public OrderServiceTest()
        {
            this._guidList = new List<Guid>
            {
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0191"),
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0192"),
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0193")
            };

            CreateOrdersListStub();
            CreateProductsListStub();

            this._orderRespositoryMock = new Mock<IOrderRepository>();
            this._orderRespositoryMock
                .Setup(o => o.GetOrdersInDateRange(It.IsAny<DateRange>()))
                .Returns(this._ordersInDateRangeStub);

            this._productRepositoryMock = new Mock<IProductRepository>();
            this._productRepositoryMock
                .Setup(p => p.GetProducts())
                .Returns(this._getProductsListStub);

            for (int i = 0; i < this._getProductsListStub.Count; i++)
            { 
                this._productRepositoryMock
                    .Setup(p => p.GetProductById(this._getProductsListStub[i].Id))
                    .Returns(this._getProductsListStub[i]);
            }


            this._orderService = new OrderService(this._orderRespositoryMock.Object, this._productRepositoryMock.Object);
        }    

        [InlineData("2020-03-04", "2020-05-06", 1)]
        [InlineData("2020-03-12", "2020-03-12", 2)]
        [InlineData("2020-03-11", "2020-03-13", 3)]
        [Theory(DisplayName = "Feature: OrderService. | Given: ValidDateRange. | When: GetMostSoldProductInDateRange. | Should: Return most sold product for date range.")]

        public void GetMostSoldProductInDateRange_ValidDateRange_ShouldReturnMostSoldProductForDateRange(string initialDateString, string finalDateString, int numberOfProducts)
        {
            // Arrange
            List<Product> expectedProducts = new List<Product>
            {
                this._getProductsListStub[1],
                this._getProductsListStub[2],
                this._getProductsListStub[0]
            };

            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse(initialDateString),
                FinalDate = DateTime.Parse(finalDateString)
            };

            // Act
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNMostSoldProductsInDateRange(dateRange, numberOfProducts);

            // Assert
            mostSoldProducts.Should().BeEquivalentTo(expectedProducts.Take(numberOfProducts));
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);

            for (int i = 0; i < numberOfProducts; i++)
            {
                this._productRepositoryMock.Verify(p => p.GetProductById(expectedProducts[i].Id), Times.Once);
            }
        }

        [InlineData("2020-03-04", "2020-05-06", 1)]
        [InlineData("2020-03-12", "2020-03-12", 2)]
        [InlineData("2020-03-11", "2020-03-13", 3)]
        [Theory(DisplayName = "Feature: OrderService. | Given: ValidDateRange. | When: GetLessSoldProductInDateRange. | Should: Return less sold products for date range.")]

        public void GetLessSoldProductInDateRange_ValidDateRange_ShouldReturnMostSoldProductForDateRange(string initialDateString, string finalDateString, int numberOfProducts)
        {
            // Arrange
            List<Product> expectedProducts = new List<Product>
            {
                this._getProductsListStub[0],
                this._getProductsListStub[2],
                this._getProductsListStub[1]
            };

            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse(initialDateString),
                FinalDate = DateTime.Parse(finalDateString)
            };

            // Act
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNLessSoldProductsInDateRange(dateRange, numberOfProducts);

            // Assert
            mostSoldProducts.Should().BeEquivalentTo(expectedProducts.Take(numberOfProducts));
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);

            for (int i = 0; i < numberOfProducts; i++)
            {
                this._productRepositoryMock.Verify(p => p.GetProductById(expectedProducts[i].Id), Times.Once);
            }
        }

        private void CreateProductsListStub()
        {
            this._getProductsListStub = new List<Product>
            {
                new Product(_guidList[0])
                {
                    Name = "boring product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("tire", "grams", 5 , new Material("latex", "malleable material")),
                        new ProductPart ("battery", "grams", 1 , new Material("copper", "electronegative element"))
                    }
                },
                new Product(_guidList[1])
                {
                    Name = "ultra cool product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("button", "grams", 10 , new Material("adamantium", "world's tougher material")),
                        new ProductPart ("lighting bulb", "grams", 1 , new Material("uranium", "radioactive material")),
                        new ProductPart ("power switch", "grams", 5 , new Material("carbon", "a nice conductor"))
                    }
                },
                new Product(_guidList[2])
                {
                    Name = "new product",
                    ProductParts = new List<ProductPart>
                    {
                        new ProductPart ("window", "grams", 300 , new Material("glass", "Extremely fragile"))
                    }
                }
            };
        }

        private void CreateOrdersListStub()
        {
            this._ordersInDateRangeStub = new List<Order>
            {
                new Order
                {
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = this._guidList[2],
                            Quantity = 3,
                            UnitPrice = 5
                        },
                        new OrderItem
                        {
                            ProductId = this._guidList[1],
                            Quantity = 10,
                            UnitPrice = 2
                        },
                        new OrderItem
                        {
                            ProductId = this._guidList[0],
                            Quantity = 1,
                            UnitPrice = 30
                        },
                        new OrderItem
                        {
                            ProductId = this._guidList[2],
                            Quantity = 2,
                            UnitPrice = 60
                        }
                    },
                    OrderDate = new DateTime(2020, 3, 12)
                }
            };
        }
    }
}
