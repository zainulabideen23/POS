// Modules/Identity/POS.Identity.Infrastructure/Configurations/PermissionConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Identity.Domain.Entities;


namespace POS.Identity.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // Primary Key is handled by convention (Id), but good to be explicit
        builder.HasKey(p => p.Id);

        // Validations & Constraints
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100); // Prevents nvarchar(max)

        builder.Property(p => p.Description)
            .HasMaxLength(250);

        //Ensure Permission names are unique
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}