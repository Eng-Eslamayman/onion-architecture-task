using System;

namespace Core.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string MobileNumber { get; set; }
        public Guid InviterId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Invitation(string mobileNumber, Guid inviterId)
        {
            Id = Guid.NewGuid();
            MobileNumber = mobileNumber;
            InviterId = inviterId;
            CreatedAt = DateTime.UtcNow;
        }

        public static Invitation NewInvitation(string mobileNumber, Guid inviterId)
        {
            return new Invitation(mobileNumber, inviterId);
        }
    }
}
