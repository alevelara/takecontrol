using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Infrastructure.Persistence.Data;

namespace Takecontrol.Emails.Infrastructure.Persistence.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("templates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TemplateType)
            .HasConversion<string>();

        builder.Property(x => x.Payload)
            .HasMaxLength(int.MaxValue)
            .IsUnicode(true);

        builder.Property(x => x.Language)
            .HasMaxLength(10);

        builder.HasData(new Template[] { TemplateFactory.WelcomeTemplate });
    }
}
