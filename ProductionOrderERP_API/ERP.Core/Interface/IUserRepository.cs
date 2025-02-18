

using Microsoft.AspNetCore.Identity.Data;
using ProductionOrderERP_API.ERP.Application.DTO;
using ProductionOrderERP_API.ERP.Core.Entity;

namespace ProductionOrderERP_API.ERP.Core.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserAsync(int userId);
        Task<List<User>> GetUsersAsync();
        Task<List<UserType>> GetUserTypesAsync();
        Task<User> UpdateUserAsync(User user);
        Task<User> ValidateUser(Application.DTO.LoginRequest loginRequest);
    }
}