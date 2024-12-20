using Core.Entities;

namespace Core.Interfaces
{
    public interface IJWTGenerator
    {
        string GenerateAccessToken(Account account, Device device);
        string GenerateRefreshToken(Account account, Device device);
    }
}
