using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductApp.Controllers;
using ProductApp.Models;
using ProductApp.Services;
using Xunit;

namespace ProductApp.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void GetProducts_ReturnsListWithCurrency()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            mockService
                .Setup(s => s.GetAll())
                .Returns(
                    new List<Product>
                    {
                        new Product
                        {
                            Id = 1,
                            Name = "Test",
                            Price = 100,
                        },
                    }
                );

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["DefaultCurrency"]).Returns("SAR");

            var controller = new ProductController(mockService.Object, mockConfig.Object);

            // Act
            var result = controller.GetProducts() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var value = result.Value as ProductResponse;

            Assert.NotNull(value);
            Assert.Equal("SAR", value.Currency);
            Assert.Single(value.Products);
        }

        [Fact]
        public void GetById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var product = new Product { Id = 1, Name = "Test", Price = 100 };
            mockService.Setup(s => s.GetById(1)).Returns(product);

            var controller = new ProductController(mockService.Object, Mock.Of<IConfiguration>());

            // Act
            var result = controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var value = result.Value as Product;
            Assert.NotNull(value);
            Assert.Equal(1, value.Id);
            Assert.Equal("Test", value.Name);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.GetById(1)).Returns((Product)null);

            var controller = new ProductController(mockService.Object, Mock.Of<IConfiguration>());

            // Act
            var result = controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_CreatesProductSuccessfully()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var newProduct = new Product { Id = 1, Name = "New Product", Price = 200 };
            mockService.Setup(s => s.Add(It.IsAny<Product>())).Verifiable();

            var controller = new ProductController(mockService.Object, Mock.Of<IConfiguration>());

            // Act
            var result = controller.Post(newProduct) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("GetById", result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);
            mockService.Verify(s => s.Add(It.IsAny<Product>()), Times.Once);
        }
    }
}

