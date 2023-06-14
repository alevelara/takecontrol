using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Matches.Domain.Models.MatchPlayers;

namespace Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Configurations;

public class MatchPlayerConfiguration : IEntityTypeConfiguration<MatchPlayer>
{
    public void Configure(EntityTypeBuilder<MatchPlayer> builder)
    {
        builder.ToTable("matches_players");

        builder.HasKey(c => new { c.PlayerId, c.MatchId });

        builder.HasOne(c => c.Match)
            .WithMany(mp => mp.MatchPlayers)
            .HasForeignKey(mp => mp.MatchId);
    }
}
