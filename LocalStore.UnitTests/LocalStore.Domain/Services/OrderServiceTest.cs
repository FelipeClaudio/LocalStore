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
    }
}
