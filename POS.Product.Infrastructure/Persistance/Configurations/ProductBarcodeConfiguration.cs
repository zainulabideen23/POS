using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure.Persistance.Configurations
{
    public class ProductBarcodeConfiguration : IEntityTypeConfiguration<ProductBarcode>
    {
        public void Configure(EntityTypeBuilder<ProductBarcode> builder)
        {
            builder.ToTable("ProductBarcodes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Barcode)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.Barcodes)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Barcode)
                .IsUnique();
        }
    }
}
