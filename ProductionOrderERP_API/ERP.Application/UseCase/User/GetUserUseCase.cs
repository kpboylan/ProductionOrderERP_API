using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GetUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Execute(int userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            return _mapper.Map<GetUserResponse>(user);
        }
    }
}
