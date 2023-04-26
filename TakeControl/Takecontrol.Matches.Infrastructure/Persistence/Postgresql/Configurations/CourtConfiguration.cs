using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Matches.Domain.Models.Courts;

namespace Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Configurations;

public class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.ToTable("courts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ClubId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();
    }
}
