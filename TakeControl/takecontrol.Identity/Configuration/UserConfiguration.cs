using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Identity.Models;

namespace takecontrol.Identity.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public static readonly Guid AdministratorUserId = Guid.NewGuid();
    public static readonly Guid PlayerUserId1 = Guid.NewGuid();
    public static readonly Guid PlayerUserId2 = Guid.NewGuid();
    public static readonly Guid ClubUserId = Guid.NewGuid();

    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = AdministratorUserId,
                Email = "alevelara@gmail.com",
                NormalizedEmail = "alevelara@gmail.com".ToUpper(),
                UserName = "alevelara",
                Name = "Alejandro",
                NormalizedUserName = "alevelara".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Administrator,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ApplicationUser
            {
                Id = PlayerUserId1,
                Email = "alevelara@localhost.com",
                NormalizedEmail = "alevelara@localhost.com".ToUpper(),
                Name = "Alberto",
                UserName = "antgonmar",
                NormalizedUserName = "antogonmar".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Player,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ApplicationUser
            {
                Id = ClubUserId,
                Email = "club@localhost.com",
                NormalizedEmail = "club@localhost.com".ToUpper(),
                Name = "PadelClubTest",
                UserName = "antgonmar2",
                NormalizedUserName = "antogonmar2".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Club,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new ApplicationUser
            {
                Id = PlayerUserId2,
                Email = "player2@gmail.com",
                NormalizedEmail = "player2@gmail.com".ToUpper(),
                Name = "player 2",
                UserName = "player2",
                NormalizedUserName = "player2".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Player,
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );
    }
}
