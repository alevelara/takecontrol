using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace takecontrol.Identity.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "d810a79b-f2a8-429b-be84-5d4d6943308e",
                Name = "Administrator",
                NormalizedName = "Administrator"
            },
            new IdentityRole
            {
                Id = "57ce438d-ae66-4e90-a8d1-cf5929eaf163",
                Name = "Player",
                NormalizedName = "Player"
            },
            new IdentityRole
            {
                Id = "6d4ce97e-9801-4881-99e1-2726d0133a35",
                Name = "Club",
                NormalizedName = "Club"
            });
    }
}
