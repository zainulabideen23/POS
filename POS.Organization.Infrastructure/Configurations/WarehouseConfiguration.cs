using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Organization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Infrastructure.Configurations
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(w => new { w.TenantId, w.Code }).IsUnique();
            builder.HasIndex(w => new { w.TenantId, w.Name }).IsUnique();
        }
    }
}
