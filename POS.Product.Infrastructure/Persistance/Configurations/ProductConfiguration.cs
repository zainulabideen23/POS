using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Product.Domain.Entities;

namespace POS.Product.Infrastructure.Persistance.Configurations
{
    public class ProductConfiguration
        : IEntityTypeConfiguration<product>
    {
        public void Configure(
            EntityTypeBuilder<product> builder)
        {
            builder.ToTable("Products");

            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.TenantId)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.BrandId)
                .IsRequired();

            builder.Property(x => x.UnitId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.SKU)
                .HasMaxLength(100);

            builder.Property(x => x.PurchasePrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.SellingPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.TaxRateId)
                .IsRequired(false);

            builder.Property(x => x.ReorderLevel)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.TrackInventory)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            // Product -> Category
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product -> Brand
            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product -> Unit
            builder.HasMany(x => x.Variants)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product -> ProductVariants
            builder.HasMany(x => x.Variants)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // SKU unique within Tenant + Branch
            builder.HasIndex(x => new
            {
                x.TenantId,
                x.SKU
            })
            .IsUnique()
            .HasFilter("[SKU] IS NOT NULL");
        }
    }
}