using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Application.Interfaces.UnitInterfaces;
using POS.Product.Infrastructure.Persistance;
using POS.Product.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplciationDbContext _db;

        public ICategoryRepository Categories { get; }
        public IBrandRepository Brands { get; }

        public IUnitRepository Units { get; }

        public UnitOfWork(
            ApplciationDbContext db)
        {
            _db = db;

            Categories = new CategoryRepository(db);
            Brands = new BrandRepository(db);
            Units = new UnitRepository(db);
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(
                cancellationToken);
        }
    }
}
