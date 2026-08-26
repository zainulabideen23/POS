using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Domain.Entities
{
    public class POSRegister
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid BranchId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public ICollection<RegisterSession> RegisterSessions { get; set; } = new List<RegisterSession>();
    }
}
