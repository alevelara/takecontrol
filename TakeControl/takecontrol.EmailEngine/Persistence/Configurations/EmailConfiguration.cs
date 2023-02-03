﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Emails.Enums;
using takecontrol.Domain.Models.Players.Enums;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.EmailEngine.Persistence.Configurations;

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
