﻿using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetActiveProductsUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetActiveProductsUseCase(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductResponse>> Execute()
        {
            try
            {
                var products = await _productRepository.GetActiveProductsAsync();
                var productDtos = _mapper.Map<List<GetProductResponse>>(products);

                return productDtos;
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}