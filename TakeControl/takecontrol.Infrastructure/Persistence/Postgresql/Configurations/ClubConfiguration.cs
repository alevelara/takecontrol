using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class ClubConfiguration : IEntityTypeConfiguration<Club> { 
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.HasOne(c => c.Address)
            .WithOne(a => a.Club)
            .HasForeignKey<Club>(c => c.AddresId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);        
    }
   
}
