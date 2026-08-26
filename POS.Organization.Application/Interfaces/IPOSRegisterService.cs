using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using POS.Organization.Application.DTOs;

namespace POS.Organization.Application.Interfaces
{
    public interface IPOSRegisterService
    {
        Task<POSRegisterResponse> CreateRegisterAsync(CreatePOSRegisterRequest request, CancellationToken cancellationToken = default);
        Task<POSRegisterResponse> GetRegisterByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<POSRegisterResponse>> GetAllRegistersAsync(CancellationToken cancellationToken = default);
        Task UpdateRegisterAsync(Guid id, UpdatePOSRegisterRequest request, CancellationToken cancellationToken = default);
        Task DeleteRegisterAsync(Guid id, CancellationToken cancellationToken = default);
    }
}