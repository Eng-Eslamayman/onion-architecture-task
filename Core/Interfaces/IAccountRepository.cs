using Core.Entities;

namespace Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByMobileNumberAsync(string mobileNumber);
        Task SaveAsync(Account account);
        Task<bool> IsAdminAsync(string mobileNumber);
    }
}
