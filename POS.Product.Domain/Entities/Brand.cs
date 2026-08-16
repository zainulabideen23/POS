using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;


        // Navigation
        public ICollection<product> Products { get; set; }
            = new List<product>();
    }
}

