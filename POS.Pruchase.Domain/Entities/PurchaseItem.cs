using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Pruchase.Domain.Entities
{
    public class PurchaseItem
    {
        // Primary Key
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid PurchaseId { get; set; }
        public Guid ProductVariantId { get; set; }

        // Properties
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        public Purchase Purchase { get; set; }
        public ICollection<PurchaseReturnItem> PurchaseReturnItems { get; set; } = new List<PurchaseReturnItem>();
    }
}
