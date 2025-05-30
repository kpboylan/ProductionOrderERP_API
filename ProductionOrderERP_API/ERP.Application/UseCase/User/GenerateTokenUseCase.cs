﻿using Microsoft.IdentityModel.Tokens;
using ProductionOrderERP_API.ERP.Core.Entity;
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

        public GenerateTokenUseCase(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:SecretKey"];
            _expiryDurationInMinutes = double.Parse(configuration["JwtSettings:ExpiryDurationInMinutes"]);
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
        }

        public virtual async Task<string> Execute(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var claims = new[]
                {
                //new Claim(ClaimTypes.Name, user.Username),
                //new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                //new Claim(ClaimTypes.Role, user.UserType.Type)
                    new Claim("name", user.Username),
                    new Claim("sub", user.UserID.ToString()),
                    new Claim("role", user.UserType.Type)
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
    }
}