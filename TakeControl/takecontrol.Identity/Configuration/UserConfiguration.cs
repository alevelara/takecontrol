using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Identity.Models;
using takecontrol.Identity.Models.Enum;

namespace takecontrol.Identity.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = "99475a30-d391-47bc-b38a-f63329df73b5",
                Email = "alevelara@gmail.com",
                NormalizedEmail = "alevelara@gmail.com",
                UserName = "alevelara",
                Name ="Alejandro",
                NormalizedUserName = "alevelara",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Administrator
            },
            new ApplicationUser
            {
                Id = "21deff44-8079-4c23-a1a1-469735a517cc",
                Email = "alevelara@localhost.com",
                NormalizedEmail = "alevelara@localhost.com",
                Name = "Alberto",                
                UserName = "antgonmar",
                NormalizedUserName = "antogonmar",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,                
                UserType = UserType.Player
            },
            new ApplicationUser
            {
                Id = "754ac959-d37d-400d-8b32-9ec9bea22074",
                Email = "club@localhost.com",
                NormalizedEmail = "club@localhost.com",
                Name = "PadelClubTest",                
                UserName = "antgonmar2",
                NormalizedUserName = "antogonmar2",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Club
            }
            ,
            new ApplicationUser
            {
                Id = "2ed8d389-80c8-4ef5-bce3-3c7881572379",
                Email = "player2@gmail.com",
                NormalizedEmail = "player2@gmail.com",
                Name = "player 2",
                UserName = "player2",
                NormalizedUserName = "player2",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
                UserType = UserType.Player
            }
        );
    }
}
