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

    public async Task<IEnumerable<Branch>> GetByTenantIdAsync(
        int tenantId)
    {
        return await _context.Branches
            .Where(b => b.TenantId == tenantId)
            .ToListAsync();
    }
}