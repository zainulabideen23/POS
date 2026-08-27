using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Pruchase.Domain.Entities;

public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(pi => pi.UnitCost).HasColumnType("decimal(18,2)");
        builder.Property(pi => pi.DiscountAmount).HasColumnType("decimal(18,2)");
        builder.Property(pi => pi.TaxAmount).HasColumnType("decimal(18,2)");
        builder.Property(pi => pi.TotalAmount).HasColumnType("decimal(18,2)");

        builder.HasMany(pi => pi.PurchaseReturnItems)
               .WithOne(pri => pri.PurchaseItem)
               .HasForeignKey(pri => pri.PurchaseItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}