using AutoMapper;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Core.Service;

namespace ProductionOrderERP_API.ERP.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;

            user.Password = PasswordHelper.Hash(user.Password);
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<List<UserDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            var userDtos = _mapper.Map<List<UserDTO>>(users);

            return userDtos;
        }

        public async Task<UserDTO> GetUserAsync(int userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public async Task<User?> UpdateUserAsync(int userId, UserDTO user)
        {
            var existingUser = await _userRepository.GetUserAsync(userId);

            user.DateCreated = existingUser.DateCreated;
            user.DateModified = DateTime.Now;

            // Check if DTO password has change in update
            if (existingUser.Password != user.Password)
            {
                // Hash the new password
                user.Password = PasswordHelper.Hash(user.Password);
            }
            
            _mapper.Map(user, existingUser);

            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

            return updatedUser;
        }

        public async Task<User?> ValidateUser(LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUser(loginDTO);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHelper.Verify(loginDTO.Password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<List<UserTypeDTO>> GetUserTypesAsync()
        {
            var userTypes = await _userRepository.GetUserTypesAsync();
            var userTypeDtos = _mapper.Map<List<UserTypeDTO>>(userTypes);

            return userTypeDtos;
        }
    }
}
