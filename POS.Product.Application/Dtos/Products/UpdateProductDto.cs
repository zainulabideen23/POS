using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Products
{
    public class UpdateProductDto
    {
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

        public bool TrackInventory { get; set; }

    }
}
