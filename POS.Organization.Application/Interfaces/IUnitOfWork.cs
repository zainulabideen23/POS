using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IUnitOfWork 
    {
        IWarehouseRepository Warehouse { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
