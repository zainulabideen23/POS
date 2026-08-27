using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Pruchase.Domain.Entities
{
    public class PurchaseReturnItem
    {
        // Primary Key
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid PurchaseReturnId { get; set; }
        public Guid PurchaseItemId { get; set; }
        public Guid ProductVariantId { get; set; }

        // Properties
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation Properties
        public PurchaseReturn PurchaseReturn { get; set; }
        public PurchaseItem PurchaseItem { get; set; }
    }
}
