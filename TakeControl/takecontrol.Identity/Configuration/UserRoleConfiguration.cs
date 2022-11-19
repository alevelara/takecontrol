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
                RoleId = "d810a79b-f2a8-429b-be84-5d4d6943308e",
                UserId = "99475a30-d391-47bc-b38a-f63329df73b5"

            },
            new IdentityUserRole<string>
            {
                RoleId = "57ce438d-ae66-4e90-a8d1-cf5929eaf163",
                UserId = "21deff44-8079-4c23-a1a1-469735a517cc"
            },
            new IdentityUserRole<string>
            {
                RoleId = "6d4ce97e-9801-4881-99e1-2726d0133a35",
                UserId = "754ac959-d37d-400d-8b32-9ec9bea22074"
            }
        );
    }
}
