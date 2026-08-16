using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class product
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid BrandId { get; set; }

        public Guid UnitId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? SKU { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public Guid? TaxRateId { get; set; }

        public decimal ReorderLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public bool TrackInventory { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        // Navigation
        public Category Category { get; set; } = null!;

        public Brand Brand { get; set; } = null!;

        public Unit Unit { get; set; } = null!;

        public ICollection<ProductVariant> Variants { get; set; }
            = new List<ProductVariant>();
    }
}
