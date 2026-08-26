using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IUnitOfWork 
    {
        IWarehouseRepository Warehouse { get; }
        IRegisterSessionRepository RegisterSession { get; }

        IPOSRegisterRepository POSRegisters { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
