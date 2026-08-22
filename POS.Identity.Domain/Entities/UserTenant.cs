using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Domain.Entities
{
    //junction table between user and tenant
    public class UserTenant
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public bool IsActive { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
