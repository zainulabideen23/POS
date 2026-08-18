using POS.Identity.Application.Interfaces;
using POS.Identity.Infrastructure.Data;

namespace POS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PosIdentityDbContext _db;

    public ITenantRepository Tenants { get; }

    public IBranchRepository Branches { get; }

    public UnitOfWork(
        PosIdentityDbContext context)
    {
        _db = context;

        Tenants = new TenantRepository(_db);
        Branches = new BranchRepository(_db);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}