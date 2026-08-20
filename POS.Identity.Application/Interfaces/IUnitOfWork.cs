using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITenantRepository Tenants { get; }

        IBranchRepository Branches { get; }

        IPermissionRepository Permissions { get; }

        IRolePermissionRepository RolePermissions { get; }

        Task<int> SaveChangesAsync();
    }
}
