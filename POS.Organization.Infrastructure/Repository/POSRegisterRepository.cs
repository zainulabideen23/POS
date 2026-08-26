using Microsoft.EntityFrameworkCore;
using POS.Organization.Domain.Entities;
using POS.Organization.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class POSRegisterRepository : Repository<POSRegister>, IPOSRegisterRepository
{
    public POSRegisterRepository(POSOrganzationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsCodeUniqueAsync(string code, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(r => r.TenantId == tenantId && r.Code == code);

        if (excludeId.HasValue)
        {
            query = query.Where(r => r.Id != excludeId.Value);
        }

        return !await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> IsNameUniqueAsync(string name, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(r => r.TenantId == tenantId && r.Name == name);

        if (excludeId.HasValue)
        {
            query = query.Where(r => r.Id != excludeId.Value);
        }

        return !await query.AnyAsync(cancellationToken);
    }
}