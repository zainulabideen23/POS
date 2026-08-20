using POS.Identity.Application.Interfaces;
using POS.Identity.Domain.Entities;

namespace POS.Identity.Application.Services
{
    public class PermissionService
    {
        private readonly IUnitOfWork _unitOfWork ;

        public PermissionService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork =unitOfWork;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Permissions
                .GetAllAsync(cancellationToken);
        }

        public async Task<Permission?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Permissions
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<Permission> CreateAsync(
            string name,
            string description,
            CancellationToken cancellationToken = default)
        {
            var existingPermission =
                await _unitOfWork.Permissions.GetByNameAsync(
                    name,
                    cancellationToken);

            if (existingPermission != null)
            {
                throw new InvalidOperationException(
                    "Permission already exists.");
            }

            var permission = new Permission
            {
                Name = name,
                Description = description
            };

            await _unitOfWork.Permissions.AddAsync(
                permission,
                cancellationToken);

            await _unitOfWork.Permissions.SaveChangesAsync(
                cancellationToken);

            return permission;
        }
    }
}