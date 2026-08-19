using POS.Identity.Application.DTOs.Permissions;
using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;

namespace POS.Identity.Application.Services
{
    public class RolePermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolePermissionService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         

        public async Task AssignPermissionAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default)
        {
            // 1. Check permission exists
            var permission =
                await _unitOfWork.Permissions.GetByIdAsync(
                    permissionId,
                    cancellationToken);

            if (permission == null)
            {
                throw new KeyNotFoundException(
                    "Permission not found.");
            }

            // 2. Check duplicate assignment
            var exists =
                await _unitOfWork.RolePermissions.ExistsAsync(
                    roleId,
                    permissionId,
                    cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    "Permission is already assigned to this role.");
            }

            // 3. Create relationship
            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            // 4. Save
            await _unitOfWork.RolePermissions.AddAsync(
                rolePermission,
                cancellationToken);

            await _unitOfWork.RolePermissions.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<IEnumerable<PermissionDto>>
    GetPermissionsByRoleAsync(
        string roleId,
        CancellationToken cancellationToken = default)
        {
            var rolePermissions =
                await _unitOfWork.RolePermissions
                    .GetByRoleIdAsync(
                        roleId,
                        cancellationToken);

            return rolePermissions
                .Select(x => new PermissionDto
                {
                    Id = x.Permission.Id,
                    Name = x.Permission.Name,
                    Description = x.Permission.Description
                });
        }
        public async Task RemovePermissionAsync(
            string roleId,
            int permissionId,
            CancellationToken cancellationToken = default)
        {
            var exists =
                await _unitOfWork.RolePermissions.ExistsAsync(
                    roleId,
                    permissionId,
                    cancellationToken);

            if (!exists)
            {
                throw new KeyNotFoundException(
                    "Permission is not assigned to this role.");
            }

            await _unitOfWork.RolePermissions.RemoveAsync(
                roleId,
                permissionId,
                cancellationToken);

            await _unitOfWork.RolePermissions.SaveChangesAsync(
                cancellationToken);
        }
    }
}