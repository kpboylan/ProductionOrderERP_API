using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetUOMsUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetUOMsUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUOMRequest>> Execute()
        {
            var uoms = await _materialRepository.GetUOMAsync();
            return _mapper.Map<List<GetUOMRequest>>(uoms);
        }
    }
}
