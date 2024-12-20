using Core.Entitiles.Account;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByMobileNumberAsync(string mobileNumber)
        {
            return await _context.Accounts
                .Include(a => a.Devices)
                .FirstOrDefaultAsync(a => a.MobileNumber == mobileNumber);
        }

        public async Task SaveAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAdminAsync(string mobileNumber)
        {
            var account = await GetByMobileNumberAsync(mobileNumber);
            return account?.Permissions.Contains("FULL_ACCESS") ?? false;
        }
    }
}
