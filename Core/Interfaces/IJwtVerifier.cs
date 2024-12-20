using System.Security.Claims;

namespace Core.Interfaces
{
    public interface IJwtVerifier
    {
        /// <summary>
        /// Verifies the JWT token and returns a ClaimsPrincipal.
        /// </summary>
        /// <param name="token">The JWT token to verify.</param>
        /// <returns>A ClaimsPrincipal if the token is valid, or null if invalid.</returns>
        ClaimsPrincipal Verify(string token);
    }
}
