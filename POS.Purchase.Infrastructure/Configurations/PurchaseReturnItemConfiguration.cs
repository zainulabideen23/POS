using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Pruchase.Domain.Entities;

public class PurchaseReturnItemConfiguration : IEntityTypeConfiguration<PurchaseReturnItem>
{
    public void Configure(EntityTypeBuilder<PurchaseReturnItem> builder)
    {
        builder.HasKey(pri => pri.Id);

        builder.Property(pri => pri.Quantity).HasColumnType("decimal(18,2)");
        builder.Property(pri => pri.UnitCost).HasColumnType("decimal(18,2)");
        builder.Property(pri => pri.TotalAmount).HasColumnType("decimal(18,2)");
    }
}