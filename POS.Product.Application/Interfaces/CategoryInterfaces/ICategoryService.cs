using POS.Product.Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

        Task<CategoryResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CategoryResponse>> GetChildrenAsync(
            Guid parentId,
            CancellationToken cancellationToken = default);

        Task<CategoryResponse?> GetParentAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<CategoryTreeResponse>> GetTreeAsync(
            CancellationToken cancellationToken = default);

        Task<CategoryResponse> CreateAsync(
            CreateCategoryRequest request,
            CancellationToken cancellationToken = default);

        Task<CategoryResponse?> UpdateAsync(
            Guid id,
            UpdateCategoryRequest request,
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

        Task<IReadOnlyList<CategoryResponse>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default);
    }
}
