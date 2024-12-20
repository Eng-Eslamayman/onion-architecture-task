using Core.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class JwtVerifier : IJwtVerifier
    {
        private readonly string _secretKey;

        public JwtVerifier(string secretKey)
        {
            _secretKey = secretKey;
        }

        public ClaimsPrincipal Verify(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // No tolerance for token expiration
                };

                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                // Return null if token validation fails
                return null;
            }
        }
    }
}
