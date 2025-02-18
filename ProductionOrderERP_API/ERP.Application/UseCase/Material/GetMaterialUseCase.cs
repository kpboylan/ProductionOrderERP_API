using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetMaterialUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<GetMaterialResponse> Execute(int materialId)
        {
            var material = await _materialRepository.GetMaterialAsync(materialId);
            return _mapper.Map<GetMaterialResponse>(material);
        }
    }
}
