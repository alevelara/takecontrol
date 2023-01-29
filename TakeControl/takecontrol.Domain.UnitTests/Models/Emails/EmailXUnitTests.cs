using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Emails.Enums;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.Domain.UnitTests.Models.Emails;

[Trait("Category", "UnitTests")]
public class EmailXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewEmail_WhenAllFieldsArePopulated()
    {
        var emailTo = "email@tests.es";
        var subject = "subjectTest";
        var email = Email.Create(emailTo, subject, TemplateType.WELCOME);

        Assert.NotNull(email);
        Assert.NotNull(email.Id);
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
        Assert.NotNull(email.Id);
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
        Assert.NotNull(email.Id);
        Assert.Null(email.EmailTo);
        Assert.Equal(email.Subject, subject);
        Assert.Equal(EmailStatus.PENDING, email.Status);
        Assert.Equal(TemplateType.WELCOME, email.TemplateType);
    }
}
