using POS.Product.Domain.Entities;
using POS.Product.Domain.Enum;

namespace POS.Product.Application.Interfaces.UnitInterfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category, CancellationToken cancellation= default);
        Task<IEnumerable<Unit>> SearchAsync(string searchTerm, CancellationToken cancellation = default);
        Task<bool> ExistsAsync(UnitCategory category,string name,Guid? excludeId = null, CancellationToken cancellation = default);
    }
}
