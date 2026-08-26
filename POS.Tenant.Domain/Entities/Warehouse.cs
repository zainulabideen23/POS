using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Domain.Entities
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid BranchId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
