namespace Infrastructure.Data.Entities
{
    public class InvitationEntity
    {
        public Guid Id { get; set; }
        public string MobileNumber { get; set; }
        public Guid InviterId { get; set; }
        public long CreatedAt { get; set; }

        public static InvitationEntity FromDomain(Core.Entities.Invitation invitation)
        {
            return new InvitationEntity
            {
                Id = invitation.Id,
                MobileNumber = invitation.MobileNumber,
                InviterId = invitation.InviterId,
                CreatedAt = invitation.CreatedAt.Ticks
            };
        }

        public Core.Entities.Invitation ToDomain()
        {
            return new Core.Entities.Invitation(MobileNumber, InviterId)
            {
                Id = Id,
                CreatedAt = new DateTime(CreatedAt)
            };
        }
    }
}
