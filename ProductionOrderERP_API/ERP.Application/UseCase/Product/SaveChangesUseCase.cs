using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class SaveChangesUseCase
    {
        private readonly IProductRepository _productRepository;

        public SaveChangesUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Execute()
        {
            return await _productRepository.SaveChangesAsync();
        }
    }
}