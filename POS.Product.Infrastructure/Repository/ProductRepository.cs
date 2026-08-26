using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces.ProductInterfaces;
using POS.Product.Domain.Entities;
using POS.Product.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Repository
{
    public class ProductRepository
      : Repository<product>, IProductRepository
    {
        public ProductRepository(ApplciationDbContext Context)
            : base(Context)
        {
        }

        public async Task<IEnumerable<product>> GetByTenantAndBranchAsync(
            Guid tenantId,
            Guid branchId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(x =>
                    x.TenantId == tenantId &&
                    x.BranchId == branchId)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<product?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Include(x => x.Variants)
                    .ThenInclude(x => x.Unit)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IEnumerable<product>> GetVariantsAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Variants)
                    .ThenInclude(x => x.Unit)
                .Where(x => x.Id == productId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<product>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<product>();

            searchTerm = searchTerm.Trim();

            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x =>
                    x.Name.Contains(searchTerm) ||
                    (x.SKU != null &&
                     x.SKU.Contains(searchTerm)) ||
                    (x.Description != null &&
                     x.Description.Contains(searchTerm)))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<product>> GetByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x => x.CategoryId == categoryId)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<product>> GetByBrandAsync(
            Guid brandId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x => x.BrandId == brandId)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<product>> GetByUnitAsync(
            Guid unitId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x => x.UnitId == unitId)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<product?> GetBySkuAsync(
            string sku,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return null;

            sku = sku.Trim();

            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .FirstOrDefaultAsync(
                    x => x.SKU == sku,
                    cancellationToken);
        }

        public async Task<bool> SkuExistsAsync(
            string sku,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return false;

            sku = sku.Trim();

            return await _dbSet.AnyAsync(
                x =>
                    x.SKU == sku &&
                    (!excludeId.HasValue ||
                     x.Id != excludeId.Value),
                cancellationToken);
        }

        public async Task<IEnumerable<product>> GetActiveProductsAsync(
            Guid tenantId,
            Guid branchId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x =>
                    x.TenantId == tenantId &&
                    x.BranchId == branchId &&
                    x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<product>> GetInactiveProductsAsync(
            Guid tenantId,
            Guid branchId,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Unit)
                .Where(x =>
                    x.TenantId == tenantId &&
                    x.BranchId == branchId &&
                    !x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            
        }
    }
}
