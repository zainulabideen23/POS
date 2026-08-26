using Microsoft.EntityFrameworkCore;
using POS.Identity.Application.Interfaces;
using POS.Identity.Infrastructure.Data;


namespace POS.Infrastructure.Repositories;

public class TenantRepository
    : Repository<Tenant>, ITenantRepository
{
    public readonly PosIdentityDbContext _context;
    public TenantRepository(PosIdentityDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Tenant?> GetByNameAsync(
    string name,
    CancellationToken cancellationToken)
    {
        return await _context.Tenants
            .FirstOrDefaultAsync(
                t => t.Name == name,
                cancellationToken);
    }
    public async Task<Tenant?> GetTenantWithBranchesAsync(Guid id)
    {
        return await _context.Tenants
            .Include(t => t.Branches)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}