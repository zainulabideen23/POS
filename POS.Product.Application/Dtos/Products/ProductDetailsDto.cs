using System;
using System.Collections.Generic;
using System.Text;
using POS.Product.Application.Dtos.ProductVariants;

namespace POS.Product.Application.Dtos.Products
{
    public class ProductDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? SKU { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string BrandName { get; set; } = string.Empty;

        public string UnitName { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal ReorderLevel { get; set; }

        public bool TrackInventory { get; set; }

        public bool IsActive { get; set; }

        public List<ProductVariantSummaryDto> Variants { get; set; } = [];
    }
}
