using POS.Organization.Application.DTOs;
using POS.Organization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<WarehouseResponse> CreateWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken = default);

        Task<WarehouseResponse?> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<WarehouseResponse>> GetAllWarehousesAsync(CancellationToken cancellationToken = default);

        Task UpdateWarehouseAsync(Guid id, UpdateWarehouseRequest request, CancellationToken cancellationToken = default);

        Task DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
