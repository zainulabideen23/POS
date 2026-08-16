using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string SKU { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public bool IsActive { get; set; } = true;


        // Navigation
        public product Product { get; set; } = null!;

        public ICollection<ProductBarcode> Barcodes { get; set; }
            = new List<ProductBarcode>();
    }
}

