using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs
{

    public class TenantResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? LegalName { get; set; }
        public string? TaxNumber { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }

        public string Currency { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
