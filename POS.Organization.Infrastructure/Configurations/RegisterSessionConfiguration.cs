using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Organization.Domain.Entities;

public class RegisterSessionConfiguration : IEntityTypeConfiguration<RegisterSession>
{
    public void Configure(EntityTypeBuilder<RegisterSession> builder)
    {
        builder.ToTable("RegisterSessions");

        builder.HasKey(rs => rs.Id);

        // Monetary constraints
        builder.Property(rs => rs.OpeningCash)
            .HasPrecision(18, 2);
            //.IsRequired();

        builder.Property(rs => rs.ExpectedClosingCash)
            .HasPrecision(18, 2);

        builder.Property(rs => rs.ActualClosingCash)
            .HasPrecision(18, 2);

        builder.Property(rs => rs.CashDifference)
            .HasPrecision(18, 2);

        //builder.Property(rs => rs.Status)
        //    .IsRequired()
        //    .HasMaxLength(20);
    }
}