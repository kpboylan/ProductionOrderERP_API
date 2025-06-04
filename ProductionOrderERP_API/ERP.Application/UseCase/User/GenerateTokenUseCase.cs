using Microsoft.IdentityModel.Tokens;
using ProductionOrderERP_API.ERP.Core.Entity;
using ProductionOrderERP_API.ERP.Infrastructure.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductionOrderERP_API.ERP.Application.UseCase
{
    public class GenerateTokenUseCase
    {
        private readonly string _secretKey;
        private readonly double _expiryDurationInMinutes;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly TenantRepository _tenantRepository;

        public GenerateTokenUseCase(IConfiguration configuration, TenantRepository tenantRepository)
        {
            _secretKey = configuration["JwtSettings:SecretKey"];
            _expiryDurationInMinutes = double.Parse(configuration["JwtSettings:ExpiryDurationInMinutes"]);
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
            _tenantRepository = tenantRepository;
        }

        public virtual async Task<string> Execute(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var tenantName = await _tenantRepository.GetTenantNameByIdAsync(user.TenantID);

                var claims = new[]
                {
                    new Claim("name", user.Username),
                    new Claim("sub", user.UserID.ToString()),
                    new Claim("role", user.UserType.Type),
                    new Claim("tenant_id", user.TenantID.ToString()),
                    new Claim("tenant_name", tenantName.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_expiryDurationInMinutes),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Core.Helper.LogHelper.LogServiceError(this.GetType().Name, ex.Message);

                throw new ApplicationException("An error occurred while processing your request.", ex);
            }
        }

        //private async Task<string> GetTenantName(int tenantId)
        //{
        //    string? name = await _tenantRepository.GetTenantNameByIdAsync(tenantId);

        //    return name;
        //}
    }
}