using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtGenerator : IJWTGenerator
    {
        private readonly string _secretKey;

        public JwtGenerator(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]
                ?? throw new ArgumentNullException("Jwt:SecretKey", "JWT Secret Key is missing in configuration.");
        }

        public string GenerateAccessToken(Account account, Device device)
        {
            var claims = new List<Claim>
            {
                new Claim("uuid", account.Id.ToString()),
                new Claim("mobile_number", account.MobileNumber),
                new Claim("device_id", device.DeviceId),
                new Claim("permissions", string.Join(",", account.Permissions))
            };

            return GenerateToken(claims, TimeSpan.FromMinutes(5));
        }

        public string GenerateRefreshToken(Account account, Device device)
        {
            var claims = new List<Claim>
            {
                new Claim("uuid", account.Id.ToString()),
                new Claim("mobile_number",""),
                new Claim("device_id","")
            };

            return GenerateToken(claims, TimeSpan.FromDays(7));
        }

        private string GenerateToken(IEnumerable<Claim> claims, TimeSpan expiry)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(expiry),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
