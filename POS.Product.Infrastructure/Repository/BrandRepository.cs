using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Domain.Entities;
using POS.Product.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplciationDbContext _db;
        public BrandRepository(ApplciationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _db.Brands.AnyAsync(x => x.Name == name && (!excludeId.HasValue || x.Id != excludeId.Value), cancellationToken);
        }

        public async Task<IReadOnlyList<product>> GetProductsAsync(Guid brandId, CancellationToken cancellationToken = default)
        {
            return await _db.Products
           .AsNoTracking()
           .Where(x => x.BrandId == brandId)
           .OrderBy(x => x.Name)
           .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _db.Brands
                .AsNoTracking()
                .Where(x =>
                    x.Name.Contains(searchTerm) ||
                    (x.Description != null &&
                     x.Description.Contains(searchTerm)))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
