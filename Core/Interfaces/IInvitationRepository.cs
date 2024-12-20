using Core.Entities;

namespace Core.Interfaces
{
    public interface IInvitationRepository
    {
        Task<bool> HasInvitationAsync(string mobileNumber);
        Task SaveInvitationAsync(Invitation invitation);
    }
}
