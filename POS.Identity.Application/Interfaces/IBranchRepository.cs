using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<IEnumerable<Branch>> GetAllByTenantIdAsync(int tenantId, CancellationToken cancellationToken);

        Task<Branch?> GetByIdAndTenantIdAsync(int id, int tenantId);
    }
}
