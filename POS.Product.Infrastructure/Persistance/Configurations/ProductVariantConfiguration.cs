using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Persistance.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.SKU)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PurchasePrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.SellingPrice)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductVariant -> Unit
            builder.HasOne(x => x.Unit)
.WithMany()
.HasForeignKey(x => x.UnitId)
.OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.SKU)
                .IsUnique();
        }

    }
}
