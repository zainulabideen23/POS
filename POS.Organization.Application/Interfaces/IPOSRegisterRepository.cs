using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

public interface IPOSRegisterRepository : IRepository<POSRegister>
{
    Task<bool> IsCodeUniqueAsync(string code, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> IsNameUniqueAsync(string name, Guid tenantId, Guid? excludeId = null, CancellationToken cancellationToken = default);
}