using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class ValidateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public ValidateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Execute(LoginRequest loginRequest)
        {
            var user = await _userRepository.ValidateUser(loginRequest);

            if (user == null || !PasswordHelper.Verify(loginRequest.Password, user.Password))
            {
                return null;
            }

            //if (user == null || !PasswordHelper.Verify(loginRequest.Password, "$2a$11$NuEk4aTn806AV4QZfP0TqeVFWv2x9Rk9Fzema/XPLKtsu/B7oy9cy"))
            //{
            //    return null;
            //}

            return user;
        }
    }
}
