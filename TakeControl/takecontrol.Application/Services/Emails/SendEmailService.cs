using takecontrol.Application.Contracts.Emails;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Application.Contracts.Persitence.Templates;
using takecontrol.Application.Contracts.Templates;
using takecontrol.Application.Exceptions;
using takecontrol.Domain.Errors.Templates;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Emails.Enums;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.Application.Services.Emails
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ITemplateAsyncReadRepository _templateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ITemplateLoader _templateLoader;
        private readonly IAsyncWriteRepository<Email> _emailWriteRepository;

        public SendEmailService(ITemplateAsyncReadRepository templateRepository, IEmailSender emailSender, ITemplateLoader templateLoader, IAsyncWriteRepository<Email> emailWriteRepository)
        {
            _templateRepository = templateRepository;
            _emailSender = emailSender;
            _templateLoader = templateLoader;
            _emailWriteRepository = emailWriteRepository;
        }

        public async Task SendEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var isSuccesfulSent = false;
            var template = await _templateRepository.GetTemplateByTemplateType(email.TemplateType);

            if (template != null)
            {
                var emailPayload = _templateLoader.LoadTemplate(template.Payload);
                await _emailSender.SendEmailAsync(email, emailPayload, cancellationToken);
            }

            var emailStatus = isSuccesfulSent ? EmailStatus.CONFIRMED : EmailStatus.FAILED;
            email.SetEmailStatus(emailStatus);
            await _emailWriteRepository.AddAsync(email);
        }
    }
}
