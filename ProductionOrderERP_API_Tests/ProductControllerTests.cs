using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductionOrderERP_API.Controllers;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;


namespace ProductionOrderERP_API_Tests
{
    public class ProductControllerTests
    {
        //private readonly Mock<IProductService> _mockProductService;
        //private readonly Mock<IProductRepository> _mockProductRepository;
        //private readonly Mock<IMapper> _mockMapper;
        //private readonly ProductController _controller;

        //public ProductControllerTests() 
        //{
        //    _mockProductService = new Mock<IProductService>();
        //    _mockProductRepository = new Mock<IProductRepository>();
        //    _mockMapper = new Mock<IMapper>();

        //    _controller = new ProductController(
        //        _mockProductService.Object,
        //        _mockMapper.Object,
        //        _mockProductRepository.Object
        //    );
        //}

        //[Fact]
        //public async Task CreateProduct_ReturnBadRequest_WhenProductDtoIsNull()
        //{
        //    // Act
        //    var result = await _controller.CreateProduct(null);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.Equal("Product data is required.", badRequestResult.Value);
        //}

        //[Fact]
        //public async Task CreateProduct_ReturnsCreatedAtActionResult_WhenProductIsCreated()
        //{
        //    // Arrange
        //    var productDto = new ProductDTO
        //    {
        //        ProductName = "New Product",
        //        ProductCode = "TES-1234"
        //    };

        //    var productEntity = new Product
        //    {
        //        ProductId = 1,
        //        ProductName = "New Product",
        //        ProductCode = "TES-1234"
        //    };

        //    // Map ProductDTO to Product entity
        //    _mockMapper.Setup(m => m.Map<Product>(It.IsAny<ProductDTO>())).Returns(productEntity);
        //    // Pass Product Entity to service
        //    _mockProductService.Setup(s => s.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(productEntity);

        //    // Act
        //    var result = await _controller.CreateProduct(productDto);

        //    // Assert
        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        //    var returnedProduct = Assert.IsType<Product>(createdAtActionResult.Value);

        //    Assert.Equal("New Product", returnedProduct.ProductName);
        //}

        //[Fact]
        //public async Task GetAllProducts_ReturnsOkResult_WhenProductsAreAvailable()
        //{
        //    // Arrange
        //    var mockProductDtos = new List<ProductDTO>
        //    {
        //        new ProductDTO { ProductId = 1, ProductName = "Product 1", ProductCode = "Test 1" },
        //        new ProductDTO { ProductId = 2, ProductName = "Product 2", ProductCode = "Test 2" }
        //    };

        //    _mockProductService.Setup(service => service.GetAllProductsAsync())
        //        .ReturnsAsync(mockProductDtos);

        //    // Act
        //    var result = await _controller.GetAllProducts();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedProductDtos = Assert.IsAssignableFrom<List<ProductDTO>>(okResult.Value);
        //    Assert.Equal(2, returnedProductDtos.Count);
        //}
    }
}