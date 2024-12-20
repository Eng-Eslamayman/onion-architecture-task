using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Threading.Tasks;
using static Core.Exceptions.AppException;

namespace Application.UseCases
{
    public class AuthenticateHandler
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IAuthProvider _authProvider;
        private readonly IJWTGenerator _jwtGenerator;

        public AuthenticateHandler(
            IAccountRepository accountRepository,
            IInvitationRepository invitationRepository,
            IAuthProvider authProvider,
            IJWTGenerator jwtGenerator)
        {
            _accountRepository = accountRepository;
            _invitationRepository = invitationRepository;
            _authProvider = authProvider;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand command)
        {
            var mobileNumber = await _authProvider.GetVerifiedMobileNumberAsync(command.IdToken);

            var account = await ExistingAccountOrNewAccountAsync(mobileNumber, command);

            var tokens = account.Authenticate(_jwtGenerator, command.DeviceId, command.DeviceType);

            await _accountRepository.SaveAsync(account);

            return new AuthenticateResponse(tokens.AccessToken, tokens.RefreshToken);
        }

        private async Task<Account> ExistingAccountOrNewAccountAsync(string mobileNumber, AuthenticateCommand request)
        {
            var account = await _accountRepository.GetByMobileNumberAsync(mobileNumber);
            if (account != null)
                return account;

            return await CreateNewAccountAsync(mobileNumber, request.DeviceType, request.DeviceId);
        }

        private async Task<Account> CreateNewAccountAsync(string mobileNumber, string deviceType, string deviceId)
        {
            var isAdmin = await _accountRepository.IsAdminAsync(mobileNumber);
            var hasNoInvitations = !(await _invitationRepository.HasInvitationAsync(mobileNumber));

            AssertIsAdminOrInvited(mobileNumber, isAdmin, hasNoInvitations);

            return Account.NewAccount(mobileNumber, isAdmin, deviceType, deviceId);
        }

        private void AssertIsAdminOrInvited(string mobileNumber, bool isAdmin, bool hasNoInvitations)
        {
            if (!isAdmin && hasNoInvitations)
            {
                throw new RequirementException("Account not invited to use the App");
            }
        }
    }
}
