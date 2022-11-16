using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using takecontrol.Identity.Models;

namespace takecontrol.Identity.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();

        builder.HasData(
            new User
            {
                Id = "user1Id",
                Email = "alevelara@gmail.com",
                NormalizedEmail = "alevelara@gmail.com",
                Name = "Alejandro",
                SurName = "Velasco Aranda",
                UserName = "alevelara",
                NormalizedUserName = "alevelara",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
            },
            new User
            {
                Id = "user2Id",
                Email = "alevelara@localhost.com",
                NormalizedEmail = "alevelara@localhost.com",
                Name = "Antonio",
                SurName = "Gonzalez Marquez",
                UserName = "antgonmar",
                NormalizedUserName = "antogonmar",
                PasswordHash = hasher.HashPassword(null, "Password123!"),
                EmailConfirmed = true,
            }
        );
    }
}
