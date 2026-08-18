using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs
{
    public class UpdateTenantDto
    {
        public string Name { get; set; } = string.Empty;
        public string? LegalName { get; set; }
        public string? TaxNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }

        public string Currency { get; set; } = "USD";
        public string Timezone { get; set; } = "UTC";

        public bool IsActive { get; set; }

    }
}
