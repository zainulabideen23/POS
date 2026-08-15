using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.CategoryInterfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetChildrenAsync(
        Guid parentId,
        CancellationToken cancellationToken = default);

        Task<Category?> GetParentAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Category>> GetTreeAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Category>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, 
            Guid? excludeId = null, 
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Category>> GetDescendantsAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);
    }
}
