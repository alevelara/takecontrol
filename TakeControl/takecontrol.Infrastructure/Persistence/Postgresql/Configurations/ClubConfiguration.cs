using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("clubs");

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.UserId);

        builder.Property(c => c.AddresId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.Code)
            .HasMaxLength(5)
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne(c => c.Address)
            .WithOne(a => a.Club)
            .HasForeignKey<Club>(c => c.AddresId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
