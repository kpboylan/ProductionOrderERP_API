using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class CreateMaterialUseCase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public CreateMaterialUseCase(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<CreateMaterialResponse> Execute(CreateMaterialRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Map the request to a material entity
            var materialEntity = _mapper.Map<Material>(request);

            await _materialRepository.CreateMaterialAsync(materialEntity);

            // Map the entity to the response
            var response = _mapper.Map<CreateMaterialResponse>(materialEntity);

            return response;
        }
    }
}
