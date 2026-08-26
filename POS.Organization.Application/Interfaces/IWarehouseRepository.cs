using POS.Organization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<bool> IsCodeUniqueAsync(string code, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default);
        // Example of a specific method outside of standard CRUD
        Task<Warehouse?> GetWarehouseByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
