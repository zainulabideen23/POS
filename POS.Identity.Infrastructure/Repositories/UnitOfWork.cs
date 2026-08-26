using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using POS.Identity.Application.Interfaces;
using POS.Identity.Infrastructure.Data;
using POS.Identity.Infrastructure.Repositories;

namespace POS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PosIdentityDbContext _db;

    public ITenantRepository Tenants { get; }

    public IBranchRepository Branches { get; }

    public IPermissionRepository Permissions { get; }

    public IRolePermissionRepository RolePermissions { get; }

    public UnitOfWork(
        PosIdentityDbContext context)
    {
        _db = context;

        Tenants = new TenantRepository(_db);
        Branches = new BranchRepository(_db);
        Permissions = new PermissionRepository(_db);
        RolePermissions = new RolePermissionRepository(_db);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _db.Database.BeginTransactionAsync();
    }
}