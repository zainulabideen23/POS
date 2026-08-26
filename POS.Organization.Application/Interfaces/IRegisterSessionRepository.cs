using POS.Organization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IRegisterSessionRepository : IRepository<RegisterSession>
    {
        Task<bool> IsSessionOpenAsync(Guid registerId, CancellationToken cancellationToken = default);
    }
}
