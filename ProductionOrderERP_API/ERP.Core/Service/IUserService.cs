using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Service
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<GetUserTypeRequest> GetUserAsync(int userId);
        Task<List<GetUserTypeRequest>> GetUsersAsync();
        Task<List<GetUserTypeRequest>> GetUserTypesAsync();
        Task<User?> UpdateUserAsync(int userId, GetUserTypeRequest user);
        Task<User?> ValidateUser(LoginRequest loginDTO);
    }
}