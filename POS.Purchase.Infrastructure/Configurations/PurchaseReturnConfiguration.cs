using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Pruchase.Domain.Entities;

public class PurchaseReturnConfiguration : IEntityTypeConfiguration<PurchaseReturn>
{
    public void Configure(EntityTypeBuilder<PurchaseReturn> builder)
    {
        builder.HasKey(pr => pr.Id);

        // Multi-tenant query filter
        builder.HasQueryFilter(pr => EF.Property<Guid>(pr, "TenantId") == pr.TenantId);

        builder.Property(pr => pr.ReturnNumber).HasMaxLength(50).IsRequired();
        builder.Property(pr => pr.Status).HasMaxLength(50).IsRequired();
        builder.Property(pr => pr.Reason).HasMaxLength(500);
        builder.Property(pr => pr.TotalAmount).HasColumnType("decimal(18,2)");

        builder.HasMany(pr => pr.PurchaseReturnItems)
               .WithOne(pri => pri.PurchaseReturn)
               .HasForeignKey(pri => pri.PurchaseReturnId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}