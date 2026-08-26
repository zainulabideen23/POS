using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using POS.Organization.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace POS.Organization.Infrastructure.Repository
{
    public class RegisterSessionRepository : Repository<RegisterSession>, IRegisterSessionRepository
    {
        public RegisterSessionRepository(POSOrganzationDbContext context) : base(context) { }

        public async Task<bool> IsSessionOpenAsync(Guid registerId, CancellationToken cancellationToken = default)
        {
            // Checks if there is any session for this register that hasn't been closed
            return await _dbSet.AnyAsync(rs => rs.RegisterId == registerId && rs.Status == "Open", cancellationToken);
        }
    }
}
