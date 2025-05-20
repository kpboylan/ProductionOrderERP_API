using Microsoft.EntityFrameworkCore;
using Serilog;
using ProductionOrderERP_API.ERP.Core.Interface;
using ProductionOrderERP_API.ERP.Infrastructure.Persistence;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Application.DTO;
using Microsoft.Data.SqlClient;

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

        public async Task<List<GetUserResponse>> GetUsersAsync()
        {
            try
            {
                return await (from user in _context.Users
                              join userType in _context.UserTypes on user.UserTypeID equals userType.UserTypeID
                              select new GetUserResponse
                              {
                                  UserID = user.UserID,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  Type = userType.Type,
                                  Username = user.Username,
                                  Active = user.Active,

                              }).ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<List<GetUserResponse>> GetActiveUsersAsync()
        {
            try
            {
                return await (from user in _context.Users.Where(p => p.Active)
                              join userType in _context.UserTypes on user.UserTypeID equals userType.UserTypeID
                              select new GetUserResponse
                              {
                                  UserID = user.UserID,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  Type = userType.Type,
                                  Username = user.Username,
                                  Active = user.Active,

                              }).ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<GetUserResponse?> GetUserAsync(int userId)
        {
            try
            {
                return await (from user in _context.Users
                              join userType in _context.UserTypes on user.UserTypeID equals userType.UserTypeID
                              select new GetUserResponse
                              {
                                  UserID = user.UserID,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  Type = userType.Type,
                                  Username = user.Username,
                                  Password = user.Password,
                                  UserTypeID = user.UserTypeID,
                                  Active = user.Active,

                              }).Where(p => p.UserID == userId).FirstOrDefaultAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<User?> ValidateUser(LoginRequest loginRequest)
        {
            try
            {
                //return await _context.Users
                //  .Where(p => p.Username == loginRequest.LoginUsername)
                //  .FirstOrDefaultAsync();
                return await _context.Users
                .Include(u => u.UserType)
                .Where(p => p.Username == loginRequest.LoginUsername)
                .FirstOrDefaultAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            try
            {
                return await _context.UserTypes.ToListAsync();
            }
            catch (SqlException ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw new Exception("A database error occurred.", ex);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);
                throw;
            }
        }
    }
}
