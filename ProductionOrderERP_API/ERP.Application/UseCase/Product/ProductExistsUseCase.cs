using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class ProductExistsUseCase
    {
        private readonly IProductRepository _productRepository;

        public ProductExistsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Execute(int productId)
        {
            return await _productRepository.ProductExistsAsync(productId);
        }
    }
}