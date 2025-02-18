using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Execute(User user)
        {
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;
            user.Password = PasswordHelper.Hash(user.Password);

            return await _userRepository.CreateUserAsync(user);
        }
    }
}
