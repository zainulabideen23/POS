using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Unit
{
    public class ConvertUnitDto
    {
        public Guid FromUnitId { get; set; }
        public Guid ToUnitId { get; set; }
        public decimal Quantity { get; set; }
    }
}
