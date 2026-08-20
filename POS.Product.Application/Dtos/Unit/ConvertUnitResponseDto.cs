using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Unit
{
    public class ConvertUnitResponseDto
    {
        public string FromUnit { get; set; } = string.Empty;
        public decimal InputQuantity { get; set; }
        public string ToUnit { get; set; } = string.Empty;
        public decimal ResultQuantity { get; set; }
    }
}
