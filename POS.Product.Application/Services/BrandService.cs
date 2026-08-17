using POS.Product.Application.Dtos.Brand;
using POS.Product.Application.Dtos.Products;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var brand = await _unitOfWork.Brands
                .GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return false;

            brand.Activate();

            _unitOfWork.Brands.Update(brand);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }

        public async Task<BrandResponse> CreateAsync(CreateBrandRequest request, CancellationToken cancellationToken = default)
        {
            var exists = await _unitOfWork.Brands
                       .ExistsByNameAsync(
                           request.Name,
                           null,
                           cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A brand with this name already exists.");

            var brand = new Brand(
                request.Name,
                request.Description);

            await _unitOfWork.Brands.AddAsync(
                brand,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(brand);
        }

        public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var brand = await _unitOfWork.Brands
                .GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return false;

            brand.Deactivate();

            _unitOfWork.Brands.Update(brand);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var brand = await _unitOfWork.Brands
                        .GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return false;

            var products = await _unitOfWork.Brands
                .GetProductsAsync(id, cancellationToken);

            if (products.Count > 0)
            {
                throw new Exception(
                    "Brand cannot be deleted because it has products.");
            }

            _unitOfWork.Brands.Delete(brand);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }
        public async Task<IReadOnlyList<BrandResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var brands = await _unitOfWork.Brands
                .GetAllAsync(cancellationToken);

            return brands
                .Select(MapToResponse)
                .ToList();
        }
        public async Task<BrandResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var brand = await _unitOfWork.Brands
                .GetByIdAsync(id, cancellationToken);

            return brand == null
                ? null
                : MapToResponse(brand);
        }

        public async Task<IReadOnlyList<ProductResponse>> GetProductsAsync(Guid brandId, CancellationToken cancellationToken = default)
        {
            var brandExists = await _unitOfWork.Brands
                     .ExistsAsync(brandId, cancellationToken);

            if (!brandExists)
                throw new KeyNotFoundException(
                    "Brand was not found.");

            var products = await _unitOfWork.Brands
                .GetProductsAsync(
                    brandId,
                    cancellationToken);

            return products
                .Select(x => new ProductResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        public async Task<IReadOnlyList<BrandResponse>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Array.Empty<BrandResponse>();

            var brands = await _unitOfWork.Brands
                .SearchAsync(
                    searchTerm.Trim(),
                    cancellationToken);

            return brands
                .Select(MapToResponse)
                .ToList();
        }
        public async Task<BrandResponse?> UpdateAsync(Guid id, UpdateBrandRequest request, CancellationToken cancellationToken = default)
        {

            var brand = await _unitOfWork.Brands
                .GetByIdAsync(id, cancellationToken);

            if (brand == null)
                return null;

            var exists = await _unitOfWork.Brands
                .ExistsByNameAsync(
                    request.Name,
                    id,
                    cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A brand with this name already exists.");

            brand.Update(
                request.Name,
                request.Description);

            _unitOfWork.Brands.Update(brand);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(brand);
        }
        private static BrandResponse MapToResponse(Brand brand)
        {
            return new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                IsActive = brand.IsActive
            };
        }
    }
}
