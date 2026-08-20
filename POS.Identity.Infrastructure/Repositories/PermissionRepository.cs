using Microsoft.EntityFrameworkCore;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure.Data;

namespace POS.Identity.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PosIdentityDbContext _context;

        public PermissionRepository(PosIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Permission?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<Permission?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(
                    x => x.Name == name,
                    cancellationToken);
        }

        public async Task AddAsync(
            Permission permission,
            CancellationToken cancellationToken = default)
        {
            await _context.Permissions.AddAsync(
                permission,
                cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}