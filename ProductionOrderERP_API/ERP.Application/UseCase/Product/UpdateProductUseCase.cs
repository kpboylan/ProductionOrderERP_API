using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResponse> Execute(int productId, UpdateProductRequest request)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductAsync(productId);

                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {productId} not found.");
                }

                _mapper.Map(request, existingProduct);

                await _productRepository.UpdateProductAsync(existingProduct);

                var response = _mapper.Map<UpdateProductResponse>(existingProduct);

                return response;
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}