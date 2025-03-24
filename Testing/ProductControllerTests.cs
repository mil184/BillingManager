using Api.Controllers;
using Domain.Model;
using Domain.Service;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Testing
{
    public class ProductControllerTests
    {
        private readonly IProductService _productService;
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            _productService = A.Fake<IProductService>();
            _productController = new ProductController(_productService);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithProducts()
        {
            // Arrange
            var products = new List<Product>();
            A.CallTo(() => _productService.GetAll()).Returns(products);

            // Act
            var result = _productController.GetAll();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Product>>();
        }
    }
}
