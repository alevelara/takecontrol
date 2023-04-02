using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Emails.Domain.Models.Emails;

namespace Takecontrol.Emails.Infrastructure.Persistence.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.ToTable("emails");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.TemplateType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(d => d.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(d => d.EmailTo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Subject)
           .HasMaxLength(150)
           .IsRequired();
    }
}
