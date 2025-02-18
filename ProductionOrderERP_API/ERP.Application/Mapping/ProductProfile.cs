using AutoMapper;
using AutoMapper.QueryableExtensions;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, CreateProductRequest>();
            CreateMap<CreateProductResponse, Product>();
            CreateMap<Product, CreateProductResponse>();

            CreateMap<GetProductRequest, Product>();
            CreateMap<Product, GetProductRequest>();
            CreateMap<GetProductResponse, Product>();
            CreateMap<Product, GetProductResponse>();

            CreateMap<UpdateProductRequest, Product>();
            CreateMap<Product, UpdateProductRequest>();
            CreateMap<UpdateProductResponse, Product>();
            CreateMap<Product, UpdateProductResponse>();
        }
    }
}
