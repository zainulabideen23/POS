using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Pruchase.Domain.Entities;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(p => p.Id);

        // Multi-tenant query filter
        builder.HasQueryFilter(p => EF.Property<Guid>(p, "TenantId") == p.TenantId);

        builder.Property(p => p.PurchaseNumber).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Status).HasMaxLength(50).IsRequired();

        builder.Property(p => p.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(p => p.DiscountAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.TaxAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.PaidAmount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.DueAmount).HasColumnType("decimal(18,2)");

        builder.HasMany(p => p.PurchaseItems)
               .WithOne(pi => pi.Purchase)
               .HasForeignKey(pi => pi.PurchaseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.PurchaseReturns)
               .WithOne(pr => pr.Purchase)
               .HasForeignKey(pr => pr.PurchaseId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a purchase if returns exist
    }
}