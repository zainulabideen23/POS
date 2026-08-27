using POS.Identity.Domain.Entities;

namespace POS.Identity.Application.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<bool> ExistsAsync(
            Guid roleId,
            int permissionId,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            RolePermission rolePermission,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(
            Guid roleId,
            int permissionId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<RolePermission>> GetByRoleIdAsync(
            Guid roleId,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}