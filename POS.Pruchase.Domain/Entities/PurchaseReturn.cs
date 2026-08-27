using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Pruchase.Domain.Entities
{
    public class PurchaseReturn
    {
        // Primary Key
        public Guid Id { get; set; }

        // Foreign Keys
        public Guid TenantId { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid SupplierId { get; set; }

        // Properties
        public string ReturnNumber { get; set; }
        public DateTime ReturnDate { get; set; }= DateTime.Now;
        public string Reason { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public Guid CreatedByUserId { get; set; }

        // Navigation Properties
        public Purchase Purchase { get; set; }
        public ICollection<PurchaseReturnItem> PurchaseReturnItems { get; set; } = new List<PurchaseReturnItem>();
    }
}
