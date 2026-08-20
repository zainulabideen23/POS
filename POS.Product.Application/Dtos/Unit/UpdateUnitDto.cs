using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Unit
{
    public class UpdateUnitDto
    {
        public string Name { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public decimal ConversionFactor { get; set; }
    }
}
