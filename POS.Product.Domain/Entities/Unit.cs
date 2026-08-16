using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Domain.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public decimal ConversionFactor { get; set; } = 1;


        // Navigation
        public ICollection<product> Products { get; set; }
            = new List<product>();
    }
}

