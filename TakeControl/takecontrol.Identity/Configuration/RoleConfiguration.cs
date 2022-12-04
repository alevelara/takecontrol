using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace takecontrol.Identity.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public static readonly Guid AdministratorRoleId = Guid.NewGuid();
    public static readonly Guid PlayerRoleId = Guid.NewGuid();
    public static readonly Guid ClubRoleId = Guid.NewGuid();

    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityRole<Guid>
            {
                Id = AdministratorRoleId,
                Name = "Administrator",
                NormalizedName = "Administrator"
            },
            new IdentityRole<Guid>
            {
                Id = PlayerRoleId,
                Name = "Player",
                NormalizedName = "Player"
            },
            new IdentityRole<Guid>
            {
                Id = ClubRoleId,
                Name = "Club",
                NormalizedName = "Club"
            });
    }
}
