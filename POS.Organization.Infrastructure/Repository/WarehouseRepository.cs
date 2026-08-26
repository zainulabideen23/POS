using Microsoft.EntityFrameworkCore;
using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using POS.Organization.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Infrastructure.Repository
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(POSOrganzationDbContext context) : base(context)
        {
        }

        public async Task<Warehouse?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(w => w.Code == code, cancellationToken);
        }

        public async Task<bool> IsCodeUniqueAsync(string code, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(w => w.TenantId == tenantId && w.Code == code);
            if (excludeId.HasValue)
            {
                query = query.Where(w => w.Id != excludeId.Value);
            }
            return !await query.AnyAsync(cancellationToken);
        }

        public async Task<bool> IsNameUniqueAsync(string name, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.Where(w => w.TenantId == tenantId && w.Name == name);
            if (excludeId.HasValue)
            {
                query = query.Where(w => w.Id != excludeId.Value);
            }
            return !await query.AnyAsync(cancellationToken);
        }
    }
}
