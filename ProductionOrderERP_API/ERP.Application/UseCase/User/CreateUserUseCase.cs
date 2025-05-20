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

        public CreateUserUseCase() { }

        public virtual async Task<User> Execute(User user)
        {
            user.DateCreated = DateTime.Now;
            user.DateModified = DateTime.Now;

            try
            {
                user.Password = PasswordHelper.Hash(user.Password);

                return await _userRepository.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }
    }
}
