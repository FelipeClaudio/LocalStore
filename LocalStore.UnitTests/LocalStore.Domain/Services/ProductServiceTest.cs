using FluentAssertions;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using LocalStore.UnitTests.Helpers;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LocalStore.UnitTests.LocalStore.Domain.Services
{
    public class ProductServiceTest
    {
        private readonly List<Product> _productListStub;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            this._productListStub = StubGenerator.GetProductsListStub();

            this._productRepositoryMock = new Mock<IProductRepository>();
            this._productRepositoryMock
                .Setup(p => p.GetProducts())
                .Returns(this._productListStub);

            foreach (Product product in this._productListStub)
            {
                this._productRepositoryMock
                    .Setup(p => p.GetProductById(product.Id))
                    .Returns(product);
            }

            this._service = new ProductService(this._productRepositoryMock.Object);
        }


        [Fact(DisplayName = "Feature: ProductService. | Given: Always. | When: GetAllProducts. | Should: Return all products.")]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Act
            IEnumerable<Product> products = this._service.GetAllProducts();

            // Assert
            products.Should().BeEquivalentTo(this._productListStub);
            this._productRepositoryMock.Verify(p => p.GetProducts(), Times.Once);
        }

        [Fact(DisplayName = "Feature: ProductService. | Given: Always. | When: GetProductById. | Should: Return chosen product.")]
        public void GetProductById_ShouldReturnChosenProduct()
        {
            // Arrange
            var expectedProduct = this._productListStub[1];

            // Act
            Product products = this._service.GetProductById(expectedProduct.Id);

            // Assert
            products.Should().BeEquivalentTo(expectedProduct);
            this._productRepositoryMock.Verify(p => p.GetProductById(expectedProduct.Id), Times.Once);
        }

        [Fact(DisplayName = "Feature: ProductService. | Given: Always. | When: InsertProduct. | Should: Insert product.")]
        public async Task InsertProduct_ShouldInsertProduct()
        {
            // Arrange
            var product = this._productListStub[2];

            // Act
            await this._service.InsertProduct(product);

            // Assert
            this._productRepositoryMock.Verify(p => p.Insert(product), Times.Once);
            this._productRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        }
    }
}
