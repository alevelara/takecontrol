using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace takecontrol.Identity.Configuration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "user1Id"

            },
            new IdentityUserRole<string>
            {
                RoleId = "2",
                UserId = "user2Id"
            }
        );
    }
}
