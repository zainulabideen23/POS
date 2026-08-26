using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class product
    {
        public Guid Id { get; private set; }

        public Guid TenantId { get; private set; }

        public Guid BranchId { get; private set; }

        public Guid CategoryId { get; private set; }

        public Guid BrandId { get; private set; }

        public Guid UnitId { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public string? SKU { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal SellingPrice { get; private set; }

        public Guid? TaxRateId { get; private set; }

        public decimal ReorderLevel { get; private set; }

        public bool IsActive { get; private set; }

        public bool TrackInventory { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        // Navigation properties
        public Category Category { get; private set; } = null!;

        public Brand Brand { get; private set; } = null!;

        public Unit Unit { get; private set; } = null!;

        public ICollection<ProductVariant> Variants { get; private set; }
            = new List<ProductVariant>();

        private product()
        {
        }

        public product(
            Guid tenantId,
            Guid branchId,
            Guid categoryId,
            Guid brandId,
            Guid unitId,
            string name,
            string? description,
            string? sku,
            decimal purchasePrice,
            decimal sellingPrice,
            Guid? taxRateId,
            decimal reorderLevel,
            bool trackInventory)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId is required.");

            if (branchId == Guid.Empty)
                throw new ArgumentException("BranchId is required.");

            if (categoryId == Guid.Empty)
                throw new ArgumentException("Category is required.");

            if (brandId == Guid.Empty)
                throw new ArgumentException("Brand is required.");

            if (unitId == Guid.Empty)
                throw new ArgumentException("Unit is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            Id = Guid.NewGuid();

            TenantId = tenantId;
            BranchId = branchId;
            CategoryId = categoryId;
            BrandId = brandId;
            UnitId = unitId;

            Name = name.Trim();
            Description = description?.Trim();
            SKU = string.IsNullOrWhiteSpace(sku) ? null : sku.Trim();

            PurchasePrice = purchasePrice;
            SellingPrice = sellingPrice;
            TaxRateId = taxRateId;
            ReorderLevel = reorderLevel;

            TrackInventory = trackInventory;

            IsActive = true;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(
            Guid categoryId,
            Guid brandId,
            Guid unitId,
            string name,
            string? description,
            string? sku,
            decimal purchasePrice,
            decimal sellingPrice,
            Guid? taxRateId,
            decimal reorderLevel,
            bool trackInventory)
        {
            if (categoryId == Guid.Empty)
                throw new ArgumentException("Category is required.");

            if (brandId == Guid.Empty)
                throw new ArgumentException("Brand is required.");

            if (unitId == Guid.Empty)
                throw new ArgumentException("Unit is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            if (purchasePrice < 0)
                throw new ArgumentException(
                    "Purchase price cannot be negative.");

            if (sellingPrice < 0)
                throw new ArgumentException(
                    "Selling price cannot be negative.");

            if (reorderLevel < 0)
                throw new ArgumentException(
                    "Reorder level cannot be negative.");

            CategoryId = categoryId;
            BrandId = brandId;
            UnitId = unitId;
            Name = name.Trim();
            Description = description?.Trim();
            SKU = string.IsNullOrWhiteSpace(sku)
                ? null
                : sku.Trim();
            PurchasePrice = purchasePrice;
            SellingPrice = sellingPrice;
            TaxRateId = taxRateId;
            ReorderLevel = reorderLevel;
            TrackInventory = trackInventory;
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
