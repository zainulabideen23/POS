
using POS.Product.Application.Dtos.Products;
using POS.Product.Application.Dtos.ProductVariants;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ProductResponseDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<ProductDetailsDto?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductVariantSummaryDto>> GetVariantsAsync(
            Guid productId,
            CancellationToken cancellationToken = default);

        Task<ProductResponseDto> CreateAsync(
            CreateProductDto dto,
            CancellationToken cancellationToken = default);

        Task<ProductResponseDto?> UpdateAsync(
            Guid id,
            UpdateProductDto dto,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> ActivateAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> DeactivateAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> GetByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> GetByBrandAsync(
            Guid brandId,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> GetByUnitAsync(
            Guid unitId,
            CancellationToken cancellationToken = default);

        Task<ProductResponseDto?> GetBySkuAsync(
            string sku,
            CancellationToken cancellationToken = default);

        Task<bool> CheckSkuExistsAsync(
            string sku,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> GetActiveProductsAsync(
            CancellationToken cancellationToken = default);

        Task<IEnumerable<ProductResponseDto>> GetInactiveProductsAsync(
            CancellationToken cancellationToken = default);
    }
}
