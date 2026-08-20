using POS.Product.Domain.Entities;
using POS.Product.Domain.Enum;

namespace POS.Product.Application.Interfaces.UnitInterfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category);
        Task<IEnumerable<Unit>> SearchAsync(string searchTerm);
        Task<bool> ExistsAsync(UnitCategory category,string name,Guid? excludeId = null);
    }
}
