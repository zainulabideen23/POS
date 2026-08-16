using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.CategoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(
        CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default);

    }
}
