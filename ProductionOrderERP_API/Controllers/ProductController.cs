using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Application.UseCase;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;
using Serilog;

namespace ProductionOrderERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ProductExistsUseCase _productExistsUseCase;
        private readonly UpdateProductUseCase _updateProductUseCase;
        private readonly GetProductByIdUseCase _getProductByIdUseCase;
        private readonly GetActiveProductsUseCase _getActiveProductsUseCase;
        private readonly GetAllProductsUseCase _getAllProductsUseCase;
        private readonly CreateProductUseCase _createProductUseCase;

        public ProductController(IProductService productService, 
            IMapper mapper, 
            IProductRepository productRepository,
            ProductExistsUseCase productExistsUseCase,
            UpdateProductUseCase updateProductUseCase,
            GetProductByIdUseCase getProductByIdUseCase,
            GetActiveProductsUseCase getActiveProductsUseCase,
            GetAllProductsUseCase getAllProductsUseCase,
            CreateProductUseCase createProductUseCase)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
            _productExistsUseCase = productExistsUseCase;
            _updateProductUseCase = updateProductUseCase;
            _getProductByIdUseCase = getProductByIdUseCase;
            _getActiveProductsUseCase = getActiveProductsUseCase;
            _getAllProductsUseCase = getAllProductsUseCase;
            _createProductUseCase = createProductUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            var response = await _createProductUseCase.Execute(product);
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var productDtos = await _getAllProductsUseCase.Execute();

                Log.Information("Hello world!");

                Log.Debug("Number of Products returned: {ProductCount}", productDtos.Count);

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveProducts()
        {
            try
            {
                var productDtos = await _getActiveProductsUseCase.Execute();

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getProduct")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            try
            {
                var productDto = await _getProductByIdUseCase.Execute(productId);

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest product, int productId)
        {
            if (product == null)
            {
                return BadRequest("Product data is null");
            }

            if (!await _productExistsUseCase.Execute(productId))
            {
                return NotFound();
            }

            await _updateProductUseCase.Execute(productId, product);

            return NoContent();
        }
    }
}
