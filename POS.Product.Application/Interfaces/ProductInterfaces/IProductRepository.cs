using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.ProductInterfaces
{
    public interface IProductRepository : IRepository<product>
    {
        Task<IEnumerable<product>> GetByTenantAndBranchAsync(
            Guid tenantId,
            Guid BranchId,
            CancellationToken cancellationToken = default);

        Task<product?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetVariantsAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetByBrandAsync(
            Guid brandId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetByUnitAsync(
            Guid unitId,
            CancellationToken cancellationToken = default);

        Task<product?> GetBySkuAsync(
            string sku,
            CancellationToken cancellationToken = default);

        Task<bool> SkuExistsAsync(
            string sku,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetActiveProductsAsync(
            Guid tenantId,
            Guid branchId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<product>> GetInactiveProductsAsync(
            Guid tenantId,
            Guid branchId,
            CancellationToken cancellationToken = default);
    }
}
