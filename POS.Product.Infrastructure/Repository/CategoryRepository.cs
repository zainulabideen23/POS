using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Domain.Entities;
using POS.Product.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Text;


namespace POS.Product.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplciationDbContext _db;

        public CategoryRepository(ApplciationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Category>> GetChildrenAsync(Guid parentId, CancellationToken cancellationToken = default)
        {
            return await _db.Categories
                .AsNoTracking()
                .Where(x =>
                    x.ParentCategoryId == parentId &&
                    x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetParentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Categories
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => x.ParentCategory)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Category>> GetTreeAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Categories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Category>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            return await _db.Categories
                .AsNoTracking()
                .Where(x =>
                    x.Name.Contains(searchTerm) ||
                    (x.Description != null &&
                     x.Description.Contains(searchTerm)))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name,Guid? excludeId = null,CancellationToken cancellationToken = default)
            {
                return await _db.Categories
                    .AnyAsync(
                        x => x.Name == name &&
                             (!excludeId.HasValue ||
                              x.Id != excludeId.Value),
                        cancellationToken);
            }

        public async Task<IReadOnlyList<Category>> GetDescendantsAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var allCategories = await _db.Categories
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var descendants = new List<Category>();

            void FindChildren(Guid parentId)
            {
                var children = allCategories
                    .Where(x => x.ParentCategoryId == parentId)
                    .ToList();

                foreach (var child in children)
                {
                    descendants.Add(child);
                    FindChildren(child.Id);
                }
            }

            FindChildren(categoryId);

            return descendants;
        }
    
    }
}
