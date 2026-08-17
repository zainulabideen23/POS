using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Brand
{
    public class BrandResponse
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool? IsActive { get; set; } 
    }
}
