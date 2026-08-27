using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Pruchase.Domain.Entities
{
    public class Purchase
    {
        // Primary Key
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid TenantId { get; set; }
        public Guid BranchId { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid SupplierId { get; set; }

        // Properties
        public string PurchaseNumber { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string Status { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
        public ICollection<PurchaseReturn> PurchaseReturns { get; set; } = new List<PurchaseReturn>();
    }
}
