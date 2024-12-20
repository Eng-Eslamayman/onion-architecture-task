using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class InvitationService
    {
        private readonly IInvitationRepository _invitationRepository;

        public InvitationService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public async Task<bool> CheckInvitationAsync(string mobileNumber)
        {
            return await _invitationRepository.HasInvitationAsync(mobileNumber);
        }

        public async Task SaveInvitationAsync(string mobileNumber, Guid inviterId)
        {
            var invitation = Invitation.NewInvitation(mobileNumber, inviterId);
            await _invitationRepository.SaveInvitationAsync(invitation);
        }
    }
}
