using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class UpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User?> Execute(int userId, GetUserRequest userDto)
        {
            var existingUser = await _userRepository.GetUserAsync(userId);

            if (existingUser == null)
            {
                return null;
            }

            userDto.DateCreated = existingUser.DateCreated;
            userDto.DateModified = DateTime.Now;

            if (existingUser.Password != userDto.Password)
            {
                userDto.Password = PasswordHelper.Hash(userDto.Password);
            }

            _mapper.Map(userDto, existingUser);

            return await _userRepository.UpdateUserAsync(existingUser);
        }
    }
}
