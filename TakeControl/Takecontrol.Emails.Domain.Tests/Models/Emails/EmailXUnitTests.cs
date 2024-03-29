﻿using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Emails.Enums;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Emails.Domain.Tests.Models.Emails;

[Trait("Category", Category.UnitTest)]
public class EmailXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewEmail_WhenAllFieldsArePopulated()
    {
        var emailTo = "email@tests.es";
        var subject = "subjectTest";
        var email = Email.Create(emailTo, subject, TemplateType.WELCOME);

        Assert.NotNull(email);
        Assert.Equal(EmailStatus.PENDING, email.Status);
        Assert.Equal(email.Subject, subject);
        Assert.Equal(email.EmailTo, emailTo);
        Assert.Equal(TemplateType.WELCOME, email.TemplateType);
    }

    [Fact]
    public void Create_Should_ReturnNewEmail_WhenSubjectIsNull()
    {
        var emailTo = "email@tests.es";
        var email = Email.Create(emailTo, null, TemplateType.WELCOME);

        Assert.NotNull(email);
        Assert.Null(email.Subject);
        Assert.Equal(EmailStatus.PENDING, email.Status);
        Assert.Equal(email.EmailTo, emailTo);
        Assert.Equal(TemplateType.WELCOME, email.TemplateType);
    }

    [Fact]
    public void Create_Should_ReturnNewEmail_WhenEmailToIsNull()
    {
        var subject = "subjectTest";
        var email = Email.Create(null, subject, TemplateType.WELCOME);

        Assert.NotNull(email);
        Assert.Null(email.EmailTo);
        Assert.Equal(email.Subject, subject);
        Assert.Equal(EmailStatus.PENDING, email.Status);
        Assert.Equal(TemplateType.WELCOME, email.TemplateType);
    }
}
