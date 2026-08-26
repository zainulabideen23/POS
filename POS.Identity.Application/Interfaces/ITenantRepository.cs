using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        Task<Tenant?> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<Tenant?> GetTenantWithBranchesAsync(Guid id);
    }
}
