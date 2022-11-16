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
                Id = "1",
                Name = "Administrator",
                NormalizedName = "Administrator"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Operator",
                NormalizedName = "Operator"
            });
    }
}
