using POS.Identity.Domain.Entities;

namespace POS.Identity.Application.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<bool> ExistsAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            RolePermission rolePermission,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(
            string roleId,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}