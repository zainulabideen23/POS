using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; private set; }
        public Guid TenantId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsActive { get; private set; } = true;
        // Navigation
        public ICollection<product> Products { get; private set; }
            = new List<product>();

        private Brand() { }

        public Brand(
            string name,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brand name is required.");
            Id = Guid.NewGuid();
            Name = name.Trim();
            Description = description;
            IsActive = true;
        }

        public void Update(
            string name,
            string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brand name is required.");
            Name = name.Trim();
            Description = description;
        }

        public void Activate()
        {
            IsActive = true;
        }
        public void Deactivate()
        {
            IsActive = false;
        }

    }
}

