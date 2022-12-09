using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Addresses.ValueObjects;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Infrastructure.Persistence.Postgresql.Initializers;

namespace takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<Club> { 
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.HasOne(c => c.Address)
            .WithOne(a => a.Club)
            .HasForeignKey<Club>(c => c.AddresId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(GetPreconfiguredClubs());
    }

    private static IEnumerable<Club> GetPreconfiguredClubs()
    {
        var clubs = DefaultData.GetClubs();
        var club1 = clubs.FirstOrDefault(c => c.Code.Equals("0001"));
        club1.Address = null;
        var club2 = clubs.FirstOrDefault(c => c.Code.Equals("0002"));
        club2.Address = null;
        var club3 = clubs.FirstOrDefault(c => c.Code.Equals("0003"));
        club3.Address = null;



        return new List<Club> { club1, club2, club3 };

    }
}
