using POS.Identity.Domain.Entities;

namespace POS.Identity.Application.Interfaces
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Permission?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Permission?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Permission permission,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}