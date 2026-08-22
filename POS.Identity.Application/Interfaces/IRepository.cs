using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface IRepository<T>
    where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
