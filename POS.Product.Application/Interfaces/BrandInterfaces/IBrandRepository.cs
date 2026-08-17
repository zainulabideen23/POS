
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.BrandInterfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<IReadOnlyList<product>> GetProductsAsync(Guid brandId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    }
}
