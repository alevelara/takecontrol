using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace takecontrol.Identity.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.AdministratorRoleId,
                UserId = UserConfiguration.AdministratorUserId
            },
            new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.ClubRoleId,
                UserId = UserConfiguration.ClubUserId
            },
            new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.PlayerRoleId,
                UserId = UserConfiguration.PlayerUserId1
            },
            new IdentityUserRole<Guid>
            {
                RoleId = RoleConfiguration.PlayerRoleId,
                UserId = UserConfiguration.PlayerUserId2
            }
        );
    }
}
