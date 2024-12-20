using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetAccountByMobileNumberAsync(string mobileNumber)
        {
            return await _accountRepository.GetByMobileNumberAsync(mobileNumber);
        }

        public async Task SaveAccountAsync(Account account)
        {
            await _accountRepository.SaveAsync(account);
        }
    }
}
