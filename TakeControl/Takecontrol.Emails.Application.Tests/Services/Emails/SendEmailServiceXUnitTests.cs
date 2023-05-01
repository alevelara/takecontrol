using Moq;
using Takecontrol.Emails.Application.Contracts.Emails;
using Takecontrol.Emails.Application.Contracts.Persitence.Emails;
using Takecontrol.Emails.Application.Contracts.Persitence.Templates;
using Takecontrol.Emails.Application.Contracts.Templates;
using Takecontrol.Emails.Application.Services.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Emails.Enums;
using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Xunit;

namespace Takecontrol.Emails.Application.Tests.Services.Emails;

[Trait("Category", "UnitTests")]
public class SendEmailServiceXUnitTests
{
    private readonly Mock<ITemplateAsyncReadRepository> _templateRepository;
    private readonly Mock<IEmailSender> _emailSender;
    private readonly Mock<ITemplateLoader> _templateLoader;
    private readonly Mock<IEmailUnitOfWork> _uow;

    public SendEmailServiceXUnitTests()
    {
        _templateLoader = new();
        _emailSender = new();
        _templateRepository = new();
        _uow = new();
    }

    [Fact]
    public async Task SendEmailAsync_Should_AddFailedEmail_WhenTemplateDoesntExist()
    {
        //Arrange
        var sender = new SendEmailService(_templateRepository.Object, _emailSender.Object, _templateLoader.Object, _uow.Object);
        Template template = null;
        var email = ApplicationTestData.CreateEmailForTest();

        _templateRepository.Setup(c => c.GetTemplateByTemplateType(It.IsAny<TemplateType>()))
            .ReturnsAsync(template);

        _uow.Setup(u => u.EmailWriteRepository().AddEmail(It.IsAny<Email>()))
            .ReturnsAsync(email);

        //Act
        await sender.SendEmailAsync(email, default);

        //Assert
        Assert.Equal(EmailStatus.FAILED, email.Status);
    }

    [Fact]
    public async Task SendEmailAsync_Should_AddFailedEmail_WhenEmailCanNotBeSent()
    {
        //Arrange
        var sender = new SendEmailService(_templateRepository.Object, _emailSender.Object, _templateLoader.Object, _uow.Object);
        Template template = ApplicationTestData.CreateTemplateForTest();
        var email = ApplicationTestData.CreateEmailForTest();

        _templateRepository.Setup(c => c.GetTemplateByTemplateType(It.IsAny<TemplateType>()))
            .ReturnsAsync(template);

        _templateLoader.Setup(t => t.LoadTemplate(It.IsAny<string>())).Returns("serializedPayload");

        _emailSender.Setup(s => s.SendEmailAsync(It.IsAny<Email>(), It.IsAny<string>(), default)).ReturnsAsync(false);

        _uow.Setup(u => u.EmailWriteRepository().AddEmail(It.IsAny<Email>()))
            .ReturnsAsync(email);

        //Act
        await sender.SendEmailAsync(email, default);

        //Assert
        Assert.Equal(EmailStatus.FAILED, email.Status);
    }

    [Fact]
    public async Task SendEmailAsync_Should_AddConfirmedEmail_WhenTemplateExists()
    {
        //Arrange
        var sender = new SendEmailService(_templateRepository.Object, _emailSender.Object, _templateLoader.Object, _uow.Object);
        Template template = ApplicationTestData.CreateTemplateForTest();
        var email = ApplicationTestData.CreateEmailForTest();

        _templateRepository.Setup(c => c.GetTemplateByTemplateType(It.IsAny<TemplateType>()))
            .ReturnsAsync(template);

        _templateLoader.Setup(t => t.LoadTemplate(It.IsAny<string>())).Returns("serializedPayload");

        _emailSender.Setup(s => s.SendEmailAsync(It.IsAny<Email>(), It.IsAny<string>(), default)).ReturnsAsync(true);

        _uow.Setup(u => u.EmailWriteRepository().AddEmail(It.IsAny<Email>()))
            .ReturnsAsync(email);

        //Act
        await sender.SendEmailAsync(email, default);

        //Assert
        Assert.Equal(EmailStatus.CONFIRMED, email.Status);
    }
}
