using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using POS.Product.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces;

namespace POS.Product.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplciationDbContext _db;

        public Repository(ApplciationDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _db.Set<T>()
                .FindAsync(
                    new object[] { id },
                    cancellationToken);
        }

        public async Task AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _db.Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _db.Set<T>()
                .FindAsync(
                    new object[] { id },
                    cancellationToken) != null;
        }
    }
}
