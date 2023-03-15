using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Domain.Models.PlayerClubs;

namespace Takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class PlayerClubConfiguration : IEntityTypeConfiguration<PlayerClub>
{
    public void Configure(EntityTypeBuilder<PlayerClub> builder)
    {
        builder.HasKey(pc => new { pc.ClubId, pc.PlayerId });

        builder.HasOne(bc => bc.Club)
            .WithMany(bc => bc.PlayerClubs)
            .HasForeignKey(bc => bc.ClubId);

        builder.HasOne(bc => bc.Player)
            .WithMany(bc => bc.PlayerClubs)
            .HasForeignKey(bc => bc.PlayerId);
    }
}
