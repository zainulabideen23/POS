using POS.Organization.Application.Interfaces;
using POS.Organization.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSOrganzationDbContext _context;

        public  IWarehouseRepository Warehouse { get; }

        public UnitOfWork(POSOrganzationDbContext context)
        {
            _context = context;
            Warehouse = new WarehouseRepository(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

       
    }
}
