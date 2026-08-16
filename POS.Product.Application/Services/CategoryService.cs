using POS.Product.Application.Dtos.Category;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET ALL
        public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var categories =
                await _unitOfWork.Categories.GetAllAsync(cancellationToken);

            return categories
                .Select(MapToResponse)
                .ToList();
        }

        // GET BY ID
        public async Task<CategoryResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var category =
                await _unitOfWork.Categories.GetByIdAsync(
                    id,
                    cancellationToken);

            return category == null
                ? null
                : MapToResponse(category);
        }

        // GET CHILDREN
        public async Task<IReadOnlyList<CategoryResponse>> GetChildrenAsync(
            Guid parentId,
            CancellationToken cancellationToken = default)
        {
            var parentExists =
                await _unitOfWork.Categories.ExistsAsync(
                    parentId,
                    cancellationToken);

            if (!parentExists)
                throw new KeyNotFoundException(
                    "Parent category was not found.");

            var children =
                await _unitOfWork.Categories.GetChildrenAsync(
                    parentId,
                    cancellationToken);

            return children
                .Select(MapToResponse)
                .ToList();
        }

        // GET PARENT
        public async Task<CategoryResponse?> GetParentAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var category =
                await _unitOfWork.Categories.GetByIdAsync(
                    id,
                    cancellationToken);

            if (category == null)
                return null;

            var parent =
                await _unitOfWork.Categories.GetParentAsync(
                    id,
                    cancellationToken);

            return parent == null
                ? null
                : MapToResponse(parent);
        }

        // GET TREE
        public async Task<IReadOnlyList<CategoryTreeResponse>> GetTreeAsync(
            CancellationToken cancellationToken = default)
        {
            var categories =
                await _unitOfWork.Categories.GetTreeAsync(
                    cancellationToken);

            var lookup = categories
                .Select(x => new CategoryTreeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentCategoryId = x.ParentCategoryId,
                    IsActive = x.IsActive
                })
                .ToDictionary(x => x.Id);

            var roots = new List<CategoryTreeResponse>();

            foreach (var category in lookup.Values)
            {
                if (category.ParentCategoryId == null)
                {
                    roots.Add(category);
                    continue;
                }

                if (lookup.TryGetValue(
                        category.ParentCategoryId.Value,
                        out var parent))
                {
                    parent.subCategories.Add(category);
                }
            }

            return roots;
        }

        // CREATE
        public async Task<CategoryResponse> CreateAsync(
            CreateCategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            // Check parent
            if (request.ParentCategoryId.HasValue)
            {
                var parentExists =
                    await _unitOfWork.Categories.ExistsAsync(
                        request.ParentCategoryId.Value,
                        cancellationToken);

                if (!parentExists)
                    throw new KeyNotFoundException(
                        "Parent category was not found.");
            }

            // Check duplicate name
            var exists =
                await _unitOfWork.Categories.ExistsByNameAsync(
                    request.Name,
                    null,
                    cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A category with this name already exists.");

            var category = new Category(
                request.Name,
                request.Description,
                request.ParentCategoryId);

            await _unitOfWork.Categories.AddAsync(
                category,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(category);
        }

        // UPDATE
        public async Task<CategoryResponse?> UpdateAsync(
            Guid id,
            UpdateCategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var category =
                await _unitOfWork.Categories.GetByIdAsync(
                    id,
                    cancellationToken);

            if (category == null)
                return null;

            // Cannot be its own parent
            if (request.ParentCategoryId == id)
            {
                throw new InvalidOperationException(
                    "A category cannot be its own parent.");
            }

            // Check parent
            if (request.ParentCategoryId.HasValue)
            {
                var parentExists =
                    await _unitOfWork.Categories.ExistsAsync(
                        request.ParentCategoryId.Value,
                        cancellationToken);

                if (!parentExists)
                    throw new KeyNotFoundException(
                        "Parent category was not found.");
            }

            // Check duplicate name
            var duplicate =
                await _unitOfWork.Categories.ExistsByNameAsync(
                    request.Name,
                    id,
                    cancellationToken);

            if (duplicate)
                throw new InvalidOperationException(
                    "A category with this name already exists.");

            category.Update(
                request.Name,
                request.Description,
                request.ParentCategoryId);

            _unitOfWork.Categories.Update(category);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return MapToResponse(category);
        }

        // DELETE
        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var category =
                await _unitOfWork.Categories.GetByIdAsync(
                    id,
                    cancellationToken);

            if (category == null)
                return false;

            var children =
                await _unitOfWork.Categories.GetChildrenAsync(
                    id,
                    cancellationToken);

            if (children.Count > 0)
            {
                throw new InvalidOperationException(
                    "Category cannot be deleted because it has child categories.");
            }

            _unitOfWork.Categories.Delete(category);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }

        // ACTIVATE
        public async Task<bool> ActivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var category =
        await _unitOfWork.Categories.GetByIdAsync(
            id,
            cancellationToken);

            if (category == null)
                return false;

            if (category.ParentCategoryId.HasValue)
            {
                var parent =
                    await _unitOfWork.Categories.GetParentAsync(
                        id,
                        cancellationToken);

                if (parent != null && !parent.IsActive)
                {
                    throw new InvalidOperationException(
                        "Cannot activate a category while its parent category is inactive.");
                }
            }

            category.Activate();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;
        }

        // DEACTIVATE
        public async Task<bool> DeactivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var category =
        await _unitOfWork.Categories.GetByIdAsync(
        id,
        cancellationToken);

            if (category == null)
                return false;

            category.Deactivate();

            var descendants =
                await _unitOfWork.Categories.GetDescendantsAsync(
                    id,
                    cancellationToken);

            foreach (var child in descendants)
            {
                child.Deactivate();
                _unitOfWork.Categories.Update(child);
            }

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return true;

        }

        // SEARCH
        public async Task<IReadOnlyList<CategoryResponse>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Array.Empty<CategoryResponse>();

            var categories =
                await _unitOfWork.Categories.SearchAsync(
                    searchTerm.Trim(),
                    cancellationToken);

            return categories
                .Select(MapToResponse)
                .ToList();
        }

        private static CategoryResponse MapToResponse(
            Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };
        }
    }
}
