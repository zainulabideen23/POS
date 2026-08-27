using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; } = DateTime.UtcNow;

        public Guid TenantId { get; private set; }

        public Category? ParentCategory { get; private set; }

        public ICollection<product> Products { get; private set; }
            = new List<product>();
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();


        private Category()
        {
        }

        public Category(
            string name,
            string? description = null,
            Guid? parentCategoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Id = Guid.NewGuid();
            Name = name.Trim();
            Description = description;
            ParentCategoryId = parentCategoryId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(
            string name,
            string? description,
            Guid? parentCategoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Name = name.Trim();
            Description = description;
            ParentCategoryId = parentCategoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}