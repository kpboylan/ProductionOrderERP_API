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

        public UpdateUserUseCase() { }

        public virtual async Task<User?> Execute(int userId, GetUserRequest userDto)
        {
            try
            {
                var userResponse = await _userRepository.GetUserAsync(userId);

                if (userResponse == null)
                {
                    return null;
                }

                userDto.DateCreated = userResponse.DateCreated;
                userDto.DateModified = DateTime.Now;

                if (userResponse.Password != userDto.Password)
                {
                    userDto.Password = PasswordHelper.Hash(userDto.Password);
                }

                _mapper.Map(userDto, userResponse);

                var userEntity = _mapper.Map<User>(userResponse);

                return await _userRepository.UpdateUserAsync(userEntity);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
