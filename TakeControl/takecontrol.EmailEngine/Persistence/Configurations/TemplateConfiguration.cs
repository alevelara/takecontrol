using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;
using Takecontrol.EmailEngine.Persistence.Data;

namespace Takecontrol.EmailEngine.Persistence.Configurations;

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
