using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Organization.Domain.Entities;

public class POSRegisterConfiguration : IEntityTypeConfiguration<POSRegister>
{
    public void Configure(EntityTypeBuilder<POSRegister> builder)
    {
        builder.ToTable("POSRegisters");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20);

        // Relationship: A POSRegister has many RegisterSessions
        builder.HasMany(p => p.RegisterSessions)
            .WithOne(rs => rs.Register)
            .HasForeignKey(rs => rs.RegisterId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a register if it has sessions
    }
}