using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Unit
{
    public class UnitResponseDto
    {
        public Guid Id { get; set; }
        public UnitCategory Category { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string BaseUnitName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public decimal ConversionFactor { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool IsActive { get; set; }
    }
}
