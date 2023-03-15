using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Domain.Models.Addresses;

namespace Takecontrol.Infrastructure.Persistence.Postgresql.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.City)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Province)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.MainAddress)
            .HasMaxLength(200)
            .IsUnicode(false)
            .IsRequired();
    }
}
