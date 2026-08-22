using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces.UnitInterfaces;
using POS.Product.Domain.Entities;
using POS.Product.Domain.Enum;
using POS.Product.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Repository
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {

        public UnitRepository(ApplciationDbContext db) : base(db) 
        {
        }

        public async Task<IEnumerable<Unit>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Unit>();

            searchTerm = searchTerm.Trim();

            return await _dbSet
                .AsNoTracking()
                .Where(x =>
                    x.Name.Contains(searchTerm) ||
                    x.ShortName.Contains(searchTerm))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(
            UnitCategory category,
            string name,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            name = name.Trim();

            return await _dbSet.AnyAsync(
                x =>
                    x.Category == category &&
                    x.Name.ToLower() == name.ToLower() &&
                    (!excludeId.HasValue || x.Id != excludeId.Value),
                cancellationToken);
        }

        public async Task<IEnumerable<Unit>> GetByCategoryAsync(UnitCategory category, CancellationToken cancellationToken)
        {
            return await _dbSet
                           .AsNoTracking()
                           .Where(x => x.Category == category)
                           .OrderBy(x => x.ConversionFactor)
                           .ThenBy(x => x.Name)
                           .ToListAsync(cancellationToken);
        }

    }
}
