using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetMaterialTypesUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialTypesUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<List<GetMaterialTypeResponse>> Execute()
        {
            var materialTypes = await _materialRepository.GetMaterialTypesAsync();
            return _mapper.Map<List<GetMaterialTypeResponse>>(materialTypes);
        }
    }
}
