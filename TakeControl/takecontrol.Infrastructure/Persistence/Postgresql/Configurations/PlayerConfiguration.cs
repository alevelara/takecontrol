using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Models.Players.Enums;

namespace Takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.NumberOfClassesInAWeek)
            .IsRequired();

        builder.Property(x => x.AvgNumberOfMatchesInAWeek)
            .IsRequired();

        builder.Property(x => x.NumberOfYearsPlayed)
            .IsRequired();

        builder.Property(x => x.PlayerLevel)
            .IsRequired();
    }
}
