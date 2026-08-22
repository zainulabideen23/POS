using Microsoft.EntityFrameworkCore;
using POS.Identity.Application.Interfaces;
using POS.Identity.Infrastructure.Data;

namespace POS.Infrastructure.Repositories;

public class BranchRepository
    : Repository<Branch>, IBranchRepository
{
    private readonly PosIdentityDbContext _context;

    public BranchRepository(PosIdentityDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Branch>> GetAllByTenantIdAsync(
        Guid tenantId, CancellationToken cancellationToken)
    {
        return await _context.Branches
            .Where(x => x.TenantId == tenantId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByIdAndTenantIdAsync(
        Guid id,
        Guid tenantId)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId);
    }
}