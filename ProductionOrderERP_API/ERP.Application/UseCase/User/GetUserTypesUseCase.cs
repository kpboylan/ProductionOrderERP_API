using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetUserTypesUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserTypesUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUserTypeResponse>> Execute()
        {
            var userTypes = await _userRepository.GetUserTypesAsync();
            return _mapper.Map<List<GetUserTypeResponse>>(userTypes);
        }
    }
}
