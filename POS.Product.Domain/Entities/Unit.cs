using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class Unit
    {
        public Guid Id { get; private set; }
        public Guid? TenantId { get; private set; }
        public UnitCategory Category { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public string ShortName { get; private set; } = string.Empty;

        public decimal ConversionFactor { get; private set; }
        public bool IsActive { get; private set; } = true;
        public bool IsSystemDefined { get; private set; }


        // Navigation
        public ICollection<ProductVariant> ProductsVaiants { get; set; }
            = new List<ProductVariant>();

        private Unit() { }

        private Unit(Guid Id, Guid? tenantId, UnitCategory category, string name, string shortName, decimal conversionFactor, bool isActive, bool isSystemDefined)
        {
            this.Id = Id;
            TenantId = tenantId;
            Category = category;
            Name = name;
            ShortName = shortName;
            ConversionFactor = conversionFactor;
            IsActive = isActive;
            IsSystemDefined = isSystemDefined;
        }
        public static Unit CreateCustom(Guid Id, Guid tenantId, UnitCategory category, string name, string shortName, decimal conversionFactor)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("Tenant is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name is required.");

            if (string.IsNullOrWhiteSpace(shortName))
                throw new ArgumentException("Unit short name is required.");

            if (conversionFactor <= 0)
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");

            return new Unit(
                new Guid(),
                tenantId,
                category,
                name.Trim(),
                shortName.Trim(),
                conversionFactor,
                false,
                true);
        }
        public static Unit CreateSystem(Guid Id, UnitCategory category, string name, string shortName,decimal conversionFactor)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name is required.");

            return new Unit(
                Id,
                null,
                category,
                name.Trim(),
                shortName.Trim(),
                conversionFactor,
                true,
                true);
        }

        public void UpdateCustom(
        string name,
        string shortName,
        decimal conversionFactor)
        {
            if (IsSystemDefined)
                throw new InvalidOperationException(
                    "System-defined units cannot be modified.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name is required.");

            if (string.IsNullOrWhiteSpace(shortName))
                throw new ArgumentException("Unit short name is required.");

            if (conversionFactor <= 0)
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");

            Name = name.Trim();
            ShortName = shortName.Trim();
            ConversionFactor = conversionFactor;
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

