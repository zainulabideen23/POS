using POS.Product.Domain.Entities;
using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Data
{
    public static class UnitSeedData
    {
        public static readonly Guid PieceId =
        Guid.Parse("10000000-0000-0000-0000-000000000001");

        public static readonly Guid DozenId =
            Guid.Parse("10000000-0000-0000-0000-000000000002");

        public static readonly Guid GramId =
            Guid.Parse("10000000-0000-0000-0000-000000000003");

        public static readonly Guid MilligramId =
            Guid.Parse("10000000-0000-0000-0000-000000000004");

        public static readonly Guid KilogramId =
            Guid.Parse("10000000-0000-0000-0000-000000000005");

        public static readonly Guid MilliliterId =
            Guid.Parse("10000000-0000-0000-0000-000000000006");

        public static readonly Guid LiterId =
            Guid.Parse("10000000-0000-0000-0000-000000000007");

        public static readonly Guid CentimeterId =
            Guid.Parse("10000000-0000-0000-0000-000000000008");

        public static readonly Guid MeterId =
            Guid.Parse("10000000-0000-0000-0000-000000000009");

        public static IEnumerable<Unit> GetUnits()
        {
            return
            [
                Unit.CreateSystem(
                PieceId,
                UnitCategory.Count,
                "Piece",
                "pcs",
                1),

            Unit.CreateSystem(
                DozenId,
                UnitCategory.Count,
                "Dozen",
                "doz",
                12),

            Unit.CreateSystem(
                GramId,
                UnitCategory.Weight,
                "Gram",
                "g",
                1),

            Unit.CreateSystem(
                MilligramId,
                UnitCategory.Weight,
                "Milligram",
                "mg",
                0.001m),

            Unit.CreateSystem(
                KilogramId,
                UnitCategory.Weight,
                "Kilogram",
                "kg",
                1000),

            Unit.CreateSystem(
                MilliliterId,
                UnitCategory.Volume,
                "Milliliter",
                "ml",
                1),

            Unit.CreateSystem(
                LiterId,
                UnitCategory.Volume,
                "Liter",
                "L",
                1000),

            Unit.CreateSystem(
                CentimeterId,
                UnitCategory.Length,
                "Centimeter",
                "cm",
                1),

            Unit.CreateSystem(
                MeterId,
                UnitCategory.Length,
                "Meter",
                "m",
                100)
            ];
        }
    }
}
