﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Identity.Constants;

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
                Name = nameof(Role.Administrator),
                NormalizedName = nameof(Role.Administrator).ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = PlayerRoleId,
                Name = nameof(Role.Player),
                NormalizedName = nameof(Role.Player).ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = ClubRoleId,
                Name = nameof(Role.Club),
                NormalizedName = nameof(Role.Club).ToUpper()
            });
    }
}
