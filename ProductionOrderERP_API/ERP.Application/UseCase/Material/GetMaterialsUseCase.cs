using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetMaterialsUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialsUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMaterialResponse>> Execute()
        {
            var materials = await _materialRepository.GetMaterialsNewAsync();
            return _mapper.Map<List<GetMaterialResponse>>(materials);
        }
    }
}
