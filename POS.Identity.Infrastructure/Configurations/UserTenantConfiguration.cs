// Modules/Identity/POS.Identity.Infrastructure/Configurations/UserTenantConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Identity.Domain.Entities;


namespace POS.Identity.Infrastructure.Configurations;

public class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
{
    public void Configure(EntityTypeBuilder<UserTenant> builder)
    {
        // Composite Primary Key
        builder.HasKey(ut => new { ut.UserId, ut.TenantId });

        // Validations & Constraints
        builder.Property(ut => ut.IsActive)
            .HasDefaultValue(true); // Default to true when a record is created

        builder.Property(ut => ut.JoinedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()"); // Database generates the timestamp

        // Relationships (Optional to define here if convention isn't enough)
        builder.HasOne(ut => ut.User)
            .WithMany(u => u.UserTenants)
            .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade); // If user is deleted, remove their tenant access
    }
}