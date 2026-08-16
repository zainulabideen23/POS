using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Persistance.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ShortName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.ConversionFactor)
                .HasPrecision(18, 4);

            builder.HasIndex(x => new { x.TenantId, x.Name })
                .IsUnique();
        }
    }
}