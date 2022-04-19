using FluentAssertions;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using LocalStore.UnitTests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LocalStore.UnitTests.LocalStore.Domain.Services
{
    public class OrderServiceTest
    {
        private readonly List<Order> _ordersInDateRangeStub;
        private readonly List<Product> _productListStub;

        private readonly Mock<IOrderRepository> _orderRespositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;

        private readonly OrderService _service;

        public OrderServiceTest()
        {
            this._ordersInDateRangeStub = StubGenerator.GetOrdersListStub();
            this._productListStub = StubGenerator.GetProductsListStub();

            this._orderRespositoryMock = new Mock<IOrderRepository>();
            this._orderRespositoryMock
                .Setup(o => o.GetOrdersInDateRange(It.IsAny<DateRange>()))
                .Returns(this._ordersInDateRangeStub);

            this._productRepositoryMock = new Mock<IProductRepository>();
            this._productRepositoryMock
                .Setup(p => p.GetProducts())
                .Returns(this._productListStub);

            for (int i = 0; i < this._productListStub.Count; i++)
            {
                this._productRepositoryMock
                    .Setup(p => p.GetProductById(this._productListStub[i].Id))
                    .Returns(this._productListStub[i]);
            }

            this._service = new OrderService(this._orderRespositoryMock.Object, this._productRepositoryMock.Object);
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
                this._productListStub[1],
                this._productListStub[2],
                this._productListStub[0]
            };

            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse(initialDateString),
                FinalDate = DateTime.Parse(finalDateString)
            };

            // Act
            IEnumerable<Product> mostSoldProducts = this._service.GetTopNMostSoldProductsInDateRange(dateRange, numberOfProducts);

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
                this._productListStub[0],
                this._productListStub[2],
                this._productListStub[1]
            };

            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse(initialDateString),
                FinalDate = DateTime.Parse(finalDateString)
            };

            // Act
            IEnumerable<Product> mostSoldProducts = this._service.GetTopNLessSoldProductsInDateRange(dateRange, numberOfProducts);

            // Assert
            mostSoldProducts.Should().BeEquivalentTo(expectedProducts.Take(numberOfProducts));
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);

            for (int i = 0; i < numberOfProducts; i++)
            {
                this._productRepositoryMock.Verify(p => p.GetProductById(expectedProducts[i].Id), Times.Once);
            }
        }

        [Fact(DisplayName = "Feature: OrderService. | Given: ValidDateRange. | When: GetAllOrdersForDateRange. | Should: Return all orders for date range.")]

        public void GetAllOrdersForDateRange_ValidDateRange_ShouldReturnMostSoldProductForDateRange()
        {
            // Arrange
            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse("2020-01-01"),
                FinalDate = DateTime.Parse("2020-12-31")
            };

            // Act
            IEnumerable<Order> ordersInDateRange = this._service.GetAllOrdersInDateRange(dateRange);

            // Assert
            ordersInDateRange.Should().BeEquivalentTo(this._ordersInDateRangeStub);
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);
        }

        [Theory(DisplayName = "Feature: OrderService. | Given: ValidDateRange. | When: GetAllOrdersForDateRange. | Should: Return all orders for date range.")]
        [InlineData(0, 30.0, 1.0)]
        [InlineData(1, 20.0, 10.0)]
        [InlineData(2, 135.0, 5.0)]
        public void GetReveneuInDateRangeForProductId_ValidDateRange_ShouldRevenueInDateRangeForProductId(int elementId, decimal expectedRevenue, decimal expectedQuantity)
        {
            // Arrange
            var dateRange = new DateRange
            {
                InitialDate = DateTime.Parse("2020-01-01"),
                FinalDate = DateTime.Parse("2020-12-31")
            };
            var selectedProduct = this._productListStub[elementId];

            // Act
            var calculatedRevenues = this._service.GetRevenuesInDateRangeForProductId(dateRange);
            var expectedResponse = calculatedRevenues.FirstOrDefault(c => c.ProductId == selectedProduct.Id);

            // Assert
            expectedResponse.Revenue.Should().Be(expectedRevenue);
            expectedResponse.Quantity.Should().Be(expectedQuantity);
            this._orderRespositoryMock.Verify(o => o.GetOrdersInDateRange(dateRange), Times.Once);
        }

        [Fact(DisplayName = "Feature: OrderService. | Given: Inexistent product. | When: InsertOrder. | Should: Not insert order.")]
        public async Task InsertOrder_InexistentProduct_ShouldNotInsertOrder()
        {
            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = Guid.Empty
                    }
                }
            };

            // Act
            await this._service.InsertOrder(order);

            // Assert
            this._productRepositoryMock.Verify(p => p.GetProducts(), Times.Once);
            this._orderRespositoryMock.Verify(o => o.Insert(It.IsAny<Order>()), Times.Never);
            this._orderRespositoryMock.Verify(o => o.SaveChangesAsync(), Times.Never);
        }

        [Fact(DisplayName = "Feature: OrderService. | Given: Existent product and order with no date. | When: InsertOrder. | Should: Insert order with current time.")]
        public async Task InsertOrder_ExistentProductAndOrderWithNoDate_ShouldInsertOrderWithCurrentTime()
        {
            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = this._productListStub[0].Id
                    }
                }
            };

            // Act
            await this._service.InsertOrder(order);

            // Assert
            this._productRepositoryMock.Verify(p => p.GetProducts(), Times.Once);
            this._orderRespositoryMock.Verify(o => o.Insert(order), Times.Once);
            this._orderRespositoryMock.Verify(o => o.SaveChangesAsync(), Times.Once);
            order.OrderDate.Should().NotBe(default);
        }

        [Fact(DisplayName = "Feature: OrderService. | Given: Existent product and order with defined date. | When: InsertOrder. | Should: Insert order with preset time.")]
        public async Task InsertOrder_ExistentProductAndOrderWithDefinedDate_ShouldInsertOrderWithPresetTime()
        {
            // Arrange
            var expectedDateTime = new DateTime(2020, 05, 15);
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = this._productListStub[0].Id
                    }
                },
                OrderDate = expectedDateTime
            };

            // Act
            await this._service.InsertOrder(order);

            // Assert
            this._productRepositoryMock.Verify(p => p.GetProducts(), Times.Once);
            this._orderRespositoryMock.Verify(o => o.Insert(order), Times.Once);
            this._orderRespositoryMock.Verify(o => o.SaveChangesAsync(), Times.Once);
            order.OrderDate.Should().Be(expectedDateTime);
        }
    }
}
