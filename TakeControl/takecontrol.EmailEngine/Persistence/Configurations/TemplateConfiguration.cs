﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.EmailEngine.Persistence.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("templates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TemplateType)
            .HasConversion(x => nameof(x),
            x => (TemplateType)Enum.Parse(typeof(TemplateType), x));

        builder.Property(x => x.Payload)
            .HasMaxLength(int.MaxValue)
            .IsUnicode(true);

        builder.Property(x => x.Language)
            .HasMaxLength(10);
    }
}