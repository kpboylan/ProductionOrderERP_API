using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateProductResponse> Execute(CreateProductRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var productEntity = _mapper.Map<Product>(request);

            await _productRepository.CreateProductAsync(productEntity);

            var response = _mapper.Map<CreateProductResponse>(productEntity);

            return response;
        }
    }
}