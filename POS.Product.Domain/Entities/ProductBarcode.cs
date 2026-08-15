using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class ProductBarcode
    {
        public Guid Id { get; set; }

        public Guid ProductVariantId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }


        // Navigation
        public ProductVariant ProductVariant { get; set; } = null!;
    }
}
