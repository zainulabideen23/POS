using POS.Product.Application.Dtos.Products;
using POS.Product.Application.Dtos.ProductVariants;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.ProductInterfaces;
using POS.Product.Domain.Entities;
using POS.Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetByTenantAndBranchAsync(
                    _currentUser.TenantId,
                    _currentUser.BranchId,
                    cancellationToken);

            return products
                .Where(x => x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<ProductResponseDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            if (product.TenantId != _currentUser.TenantId)
                return null;

            return Map(product);
        }

        public async Task<ProductDetailsDto?> GetDetailsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetDetailsAsync(
                    id,
                    cancellationToken);

            if (product == null)
                return null;

            if (product.TenantId != _currentUser.TenantId)
                return null;

            return MapDetails(product);
        }

        public async Task<IEnumerable<ProductVariantSummaryDto>>
            GetVariantsAsync(
                Guid productId,
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetVariantsAsync(
                    productId,
                    cancellationToken);

            var product = products.FirstOrDefault();

            if (product == null)
                return Enumerable.Empty<ProductVariantSummaryDto>();

            if (product.TenantId != _currentUser.TenantId)
                return Enumerable.Empty<ProductVariantSummaryDto>();

            return product.Variants
                .Where(x => x.IsActive)
                .Select(x => new ProductVariantSummaryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    UnitId = x.UnitId,
                    UnitName = x.Unit.Name,
                    SKU = x.SKU,
                    PurchasePrice = x.PurchasePrice,
                    SellingPrice = x.SellingPrice,
                    IsActive = x.IsActive
                })
                .ToList();
        }

        public async Task<ProductResponseDto> CreateAsync(
            CreateProductDto dto,
            CancellationToken cancellationToken = default)
        {
            ValidateDto(dto);

            if (!string.IsNullOrWhiteSpace(dto.SKU))
            {
                var exists =
                    await _productRepository.SkuExistsAsync(
                        dto.SKU,
                        null,
                        cancellationToken);

                if (exists)
                {
                    throw new InvalidOperationException(
                        "A product with this SKU already exists.");
                }
            }

            var product = new product(
                _currentUser.TenantId,
                _currentUser.BranchId,
                dto.CategoryId,
                dto.BrandId,
                dto.UnitId,
                dto.Name,
                dto.Description,
                dto.SKU,
                dto.PurchasePrice,
                dto.SellingPrice,
                dto.TaxRateId,
                dto.ReorderLevel,
                dto.TrackInventory);

            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return Map(product);
        }

        public async Task<ProductResponseDto?> UpdateAsync(
            Guid id,
            UpdateProductDto dto,
            CancellationToken cancellationToken = default)
        {
            ValidateDto(dto);

            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            if (product.TenantId != _currentUser.TenantId)
                return null;

            if (!string.IsNullOrWhiteSpace(dto.SKU))
            {
                var exists =
                    await _productRepository.SkuExistsAsync(
                        dto.SKU,
                        id,
                        cancellationToken);

                if (exists)
                {
                    throw new InvalidOperationException(
                        "A product with this SKU already exists.");
                }
            }

            product.Update(
                dto.CategoryId,
                dto.BrandId,
                dto.UnitId,
                dto.Name,
                dto.Description,
                dto.SKU,
                dto.PurchasePrice,
                dto.SellingPrice,
                dto.TaxRateId,
                dto.ReorderLevel,
                dto.TrackInventory);

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync();

            return Map(product);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            if (product.TenantId != _currentUser.TenantId)
                return false;

            // Soft delete.
            product.Deactivate();

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            if (product.TenantId != _currentUser.TenantId)
                return false;

            product.Activate();

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            if (product.TenantId != _currentUser.TenantId)
                return false;

            product.Deactivate();

            _productRepository.Update(product);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductResponseDto>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.SearchAsync(
                    searchTerm,
                    cancellationToken);

            return products
                .Where(x =>
                    x.TenantId == _currentUser.TenantId &&
                    x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<IEnumerable<ProductResponseDto>>
            GetByCategoryAsync(
                Guid categoryId,
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetByCategoryAsync(
                    categoryId,
                    cancellationToken);

            return products
                .Where(x =>
                    x.TenantId == _currentUser.TenantId &&
                    x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<IEnumerable<ProductResponseDto>>
            GetByBrandAsync(
                Guid brandId,
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetByBrandAsync(
                    brandId,
                    cancellationToken);

            return products
                .Where(x =>
                    x.TenantId == _currentUser.TenantId &&
                    x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<IEnumerable<ProductResponseDto>>
            GetByUnitAsync(
                Guid unitId,
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetByUnitAsync(
                    unitId,
                    cancellationToken);

            return products
                .Where(x =>
                    x.TenantId == _currentUser.TenantId &&
                    x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<ProductResponseDto?> GetBySkuAsync(
            string sku,
            CancellationToken cancellationToken = default)
        {
            var product =
                await _productRepository.GetBySkuAsync(
                    sku,
                    cancellationToken);

            if (product == null)
                return null;

            if (product.TenantId != _currentUser.TenantId)
                return null;

            return Map(product);
        }

        public Task<bool> CheckSkuExistsAsync(
            string sku,
            Guid? excludeId = null,
            CancellationToken cancellationToken = default)
        {
            return _productRepository.SkuExistsAsync(
                sku,
                excludeId,
                cancellationToken);
        }

        public async Task<IEnumerable<ProductResponseDto>>
            GetActiveProductsAsync(
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetActiveProductsAsync(
                    _currentUser.TenantId,
                    _currentUser.BranchId,
                    cancellationToken);

            return products
                .Select(Map)
                .ToList();
        }

        public async Task<IEnumerable<ProductResponseDto>>
            GetInactiveProductsAsync(
                CancellationToken cancellationToken = default)
        {
            var products =
                await _productRepository.GetInactiveProductsAsync(
                    _currentUser.TenantId,
                    _currentUser.BranchId,
                    cancellationToken);

            return products
                .Select(Map)
                .ToList();
        }

        private static void ValidateDto(
            CreateProductDto dto)
        {
            if (dto.CategoryId == Guid.Empty)
                throw new ArgumentException(
                    "Category is required.");

            if (dto.BrandId == Guid.Empty)
                throw new ArgumentException(
                    "Brand is required.");

            if (dto.UnitId == Guid.Empty)
                throw new ArgumentException(
                    "Unit is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException(
                    "Product name is required.");

            if (dto.PurchasePrice < 0)
                throw new ArgumentException(
                    "Purchase price cannot be negative.");

            if (dto.SellingPrice < 0)
                throw new ArgumentException(
                    "Selling price cannot be negative.");

            if (dto.ReorderLevel < 0)
                throw new ArgumentException(
                    "Reorder level cannot be negative.");
        }

        private static void ValidateDto(
            UpdateProductDto dto)
        {
            if (dto.CategoryId == Guid.Empty)
                throw new ArgumentException(
                    "Category is required.");

            if (dto.BrandId == Guid.Empty)
                throw new ArgumentException(
                    "Brand is required.");

            if (dto.UnitId == Guid.Empty)
                throw new ArgumentException(
                    "Unit is required.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException(
                    "Product name is required.");

            if (dto.PurchasePrice < 0)
                throw new ArgumentException(
                    "Purchase price cannot be negative.");

            if (dto.SellingPrice < 0)
                throw new ArgumentException(
                    "Selling price cannot be negative.");

            if (dto.ReorderLevel < 0)
                throw new ArgumentException(
                    "Reorder level cannot be negative.");
        }

        private static ProductResponseDto Map(
            product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,

                BrandId = product.BrandId,
                BrandName = product.Brand?.Name ?? string.Empty,

                UnitId = product.UnitId,
                UnitName = product.Unit?.Name ?? string.Empty,

                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,

                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice,

                TaxRateId = product.TaxRateId,

                ReorderLevel = product.ReorderLevel,

                TrackInventory = product.TrackInventory,

                IsActive = product.IsActive,

                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        private static ProductDetailsDto MapDetails(
            product product)
        {
            return new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,

                CategoryName =
                    product.Category?.Name ?? string.Empty,

                BrandName =
                    product.Brand?.Name ?? string.Empty,

                UnitName =
                    product.Unit?.Name ?? string.Empty,

                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice,

                ReorderLevel = product.ReorderLevel,

                TrackInventory = product.TrackInventory,

                IsActive = product.IsActive,

                Variants = product.Variants
                    .Select(x => new ProductVariantSummaryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Quantity = x.Quantity,
                        UnitId = x.UnitId,
                        UnitName = x.Unit?.Name ?? string.Empty,
                        SKU = x.SKU,
                        PurchasePrice = x.PurchasePrice,
                        SellingPrice = x.SellingPrice,
                        IsActive = x.IsActive
                    })
                    .ToList()
            };
        }
    }
}
