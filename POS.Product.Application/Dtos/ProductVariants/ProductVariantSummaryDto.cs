using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.ProductVariants
{
    public class ProductVariantSummaryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public Guid UnitId { get; set; }

        public string UnitName { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public bool IsActive { get; set; }
    }

}
