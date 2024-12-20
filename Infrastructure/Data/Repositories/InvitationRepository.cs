using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.DbContexts;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _context;

        public InvitationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasInvitationAsync(string mobileNumber)
        {
            return await _context.Set<InvitationEntity>().AnyAsync(i => i.MobileNumber == mobileNumber);
        }

        public async Task SaveInvitationAsync(Invitation invitation)
        {
            var entity = InvitationEntity.FromDomain(invitation);
            await _context.Set<InvitationEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
