using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.ToTable("matches");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ReservationId)
            .IsRequired();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.IsClosed)
            .IsRequired();

        builder.Property(c => c.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(c => c.CancelledDescription)
            .HasMaxLength(100);
    }
}
