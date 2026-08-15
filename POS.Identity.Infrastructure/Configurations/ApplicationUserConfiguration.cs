// Modules/Identity/POS.Identity.Infrastructure/Configurations/ApplicationUserConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Identity.Domain.Entities;


namespace POS.Identity.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
     ;

        // 2. Navigation Properties Setup
        // Explicitly defining the relationships ensures EF Core generates 
        // the foreign keys exactly as you intend.
        builder.HasMany(u => u.UserTenants)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        builder.HasMany(u => u.UserBranches)
            .WithOne(ub => ub.User)
            .HasForeignKey(ub => ub.UserId)
            .IsRequired();
    }
}