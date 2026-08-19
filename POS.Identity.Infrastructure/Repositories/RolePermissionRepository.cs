using Microsoft.EntityFrameworkCore;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure.Data;

namespace POS.Identity.Infrastructure.Repositories
{
    public class RolePermissionRepository
        : IRolePermissionRepository
    {
        private readonly PosIdentityDbContext _context;

        public RolePermissionRepository(
            PosIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .AnyAsync(
                    x => x.RoleId == roleId &&
                         x.PermissionId == permissionId,
                    cancellationToken);
        }

        public async Task AddAsync(
            RolePermission rolePermission,
            CancellationToken cancellationToken = default)
        {
            await _context.RolePermissions.AddAsync(
                rolePermission,
                cancellationToken);
        }

        public async Task RemoveAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default)
        {
            var rolePermission =
                await _context.RolePermissions
                    .FirstOrDefaultAsync(
                        x => x.RoleId == roleId &&
                             x.PermissionId == permissionId,
                        cancellationToken);

            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(
                    rolePermission);
            }
        }

        public async Task<IEnumerable<RolePermission>>
            GetByRoleIdAsync(
                string roleId,
                CancellationToken cancellationToken = default)
        {
            return await _context.RolePermissions
                .Include(x => x.Permission)
                .Where(x => x.RoleId == roleId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(
                cancellationToken);
        }
    }
}