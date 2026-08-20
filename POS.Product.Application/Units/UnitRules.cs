using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Units
{
    public static class UnitRules
    {
        public static string GetBaseUnitName(UnitCategory category)
        {
            return category switch
            {
                UnitCategory.Count => "Piece",
                UnitCategory.Weight => "Gram",
                UnitCategory.Volume => "Milliliter",
                UnitCategory.Length => "Centimeter",

                _ => throw new ArgumentOutOfRangeException(
                    nameof(category))
            };
        }

        public static decimal GetBaseConversionFactor(
            UnitCategory category)
        {
            return 1m;
        }
    }
}
