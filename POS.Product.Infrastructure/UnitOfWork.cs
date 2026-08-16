using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplciationDbContext _db;

        public ICategoryRepository Categories { get; }

        public UnitOfWork(
            ApplciationDbContext db,
            ICategoryRepository categoryRepository)
        {
            _db = db;

            Categories = categoryRepository;
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(
                cancellationToken);
        }
    }
}
