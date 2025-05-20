using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Application.Mapping
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile() 
        {
            CreateMap<GetMaterialTypeResponse, MaterialType>();
            CreateMap<MaterialType, GetMaterialTypeResponse>();

            CreateMap<GetUOMRequest, UOM>();
            CreateMap<UOM, GetUOMRequest>();

            CreateMap<CreateMaterialRequest, Material>();
            CreateMap<Material, CreateMaterialRequest>();
            CreateMap<CreateMaterialResponse, Material>();
            CreateMap<Material, CreateMaterialResponse>();

            CreateMap<PublishMaterialRequest, Material>();
            CreateMap<Material, PublishMaterialRequest>();
            CreateMap<PublishMaterialResponse, Material>();
            CreateMap<Material, PublishMaterialResponse>();

            CreateMap<GetMaterialRequest, Material>();
            CreateMap<Material, GetMaterialRequest>();
            CreateMap<GetMaterialResponse, Material>();
            CreateMap<Material, GetMaterialResponse>();
            CreateMap<UpdateMaterialRequest, Material>();

        }
    }
}
