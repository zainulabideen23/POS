using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        Task<Tenant?> GetTenantWithBranchesAsync(int id);
    }
}
