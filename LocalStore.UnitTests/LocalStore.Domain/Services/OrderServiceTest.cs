using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Xunit;
using Moq;
using System.Collections.Generic;
using System;
using FluentAssertions;
using LocalStore.Commons.Models;

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
                new Guid("62FA647C-AD54-4BCC-A860-E5A2664B0192")
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

            this._productRepositoryMock
                .Setup(p => p.GetProductByName("ultra cool product"))
                .Returns(this._getProductsListStub[1]);

            this._orderService = new OrderService(this._orderRespositoryMock.Object, this._productRepositoryMock.Object);
        }    

        [InlineData("2020-03-04", "2020-05-06")]
        [InlineData("2020-03-04", "2020-03-05")]
        [InlineData("2020-03-04", "2020-03-03")]
        [Theory(DisplayName = "Featurea: OrderService. | Given: ValidDateRange. | When: GetMostSoldProductInDateRange. | Should: Return most sold product.")]

        public void GetMostSoldProductInDateRange_ValidDateRange_ShouldReturnMostSoldProduct(string initialDateString, string finalDateString)
        {
            // Arrange
            Product expectedProduct = this._getProductsListStub[1];

            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse(initialDateString),
                FinalDate = DateTime.Parse(finalDateString)
            };

            // Act
            Product mostSoldProduct = this._orderService.GetMostSoldProductInDateRange(dateRange);

            // Assert
            mostSoldProduct.Should().BeEquivalentTo(expectedProduct);
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);
            this._productRepositoryMock.Verify(p => p.GetProducts(), Times.Once);
            this._productRepositoryMock.Verify(p => p.GetProductByName(this._getProductsListStub[1].Name), Times.Once);
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
                            ProductId = _guidList[1],
                            Quantity = 3,
                            UnitPrice = 5
                        },
                        new OrderItem
                        {
                            ProductId = _guidList[0],
                            Quantity = 10,
                            UnitPrice = 2
                        },
                        new OrderItem
                        {
                            ProductId = _guidList[1],
                            Quantity = 1,
                            UnitPrice = 30
                        }
                    },
                    OrderDate = new DateTime(2020, 3, 12)
                }
            };
        }
    }
}
