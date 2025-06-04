using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Core.Helper;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class ValidateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly TenantRepository _tenantRepository;

        public ValidateUserUseCase(IUserRepository userRepository, TenantRepository tenantRepository)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
        }

        public virtual async Task<User?> Execute(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userRepository.ValidateUser(loginRequest);

                if (user == null || !PasswordHelper.Verify(loginRequest.LoginPassword, user.Password))
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
