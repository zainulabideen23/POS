using POS.Product.Application.Dtos.Brand;
using POS.Product.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.BrandInterfaces
{
    public interface IBrandService
    {
        Task<IReadOnlyList<BrandResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

        Task<BrandResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProductResponseDto>> GetProductsAsync(
            Guid brandId,
            CancellationToken cancellationToken = default);

        Task<BrandResponse> CreateAsync(
            CreateBrandRequest request,
            CancellationToken cancellationToken = default);

        Task<BrandResponse?> UpdateAsync(
            Guid id,
            UpdateBrandRequest request,
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

        Task<IReadOnlyList<BrandResponse>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default);
    }
}
