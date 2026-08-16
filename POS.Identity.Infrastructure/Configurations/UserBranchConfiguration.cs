// Modules/Identity/POS.Identity.Infrastructure/Configurations/UserBranchConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Identity.Domain.Entities;

namespace POS.Identity.Infrastructure.Configurations;

public class UserBranchConfiguration : IEntityTypeConfiguration<UserBranch>
{
    public void Configure(EntityTypeBuilder<UserBranch> builder)
    {
        // 1. Configure the Composite Primary Key
        builder.HasKey(ub => new { ub.UserId, ub.BranchId });

        // 2. Configure the Relationship to ApplicationUser
        builder.HasOne(ub => ub.User)
            .WithMany(u => u.UserBranches)
            .HasForeignKey(ub => ub.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Deleting a user removes their branch access

        // Note: The relationship to the actual Branch entity will be configured 
        // in the Organization module to maintain loose coupling between domains.
    }
}