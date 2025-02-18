using Microsoft.EntityFrameworkCore;
using Serilog;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Application.DTO;

namespace ProductionOrderERP_API.ERP.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ERPContext _context;

        public UserRepository(ERPContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users
              .Where(p => p.UserID == userId)
              .FirstOrDefaultAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Log.Error("Exception: {UpdateUserAsync}", ex.Message);
            }


            return user;
        }

        public async Task<User?> ValidateUser(LoginRequest loginRequest)
        {
            return await _context.Users
              .Where(p => p.Username == loginRequest.Username)
              .FirstOrDefaultAsync();
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            return await _context.UserTypes.ToListAsync();
        }
    }
}
