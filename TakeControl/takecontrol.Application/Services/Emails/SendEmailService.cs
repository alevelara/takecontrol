﻿using takecontrol.Application.Contracts.Emails;
using takecontrol.Application.Contracts.Persitence.Emails;
using takecontrol.Application.Contracts.Persitence.Templates;
using takecontrol.Application.Contracts.Templates;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Emails.Enums;

namespace takecontrol.Application.Services.Emails
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ITemplateAsyncReadRepository _templateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ITemplateLoader _templateLoader;
        private readonly IEmailUnitOfWork _unitOfWork;

        public SendEmailService(ITemplateAsyncReadRepository templateRepository, IEmailSender emailSender, ITemplateLoader templateLoader, IEmailUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _emailSender = emailSender;
            _templateLoader = templateLoader;
            _unitOfWork = unitOfWork;
        }

        public async Task SendEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var isSuccesfulSent = false;
            var template = await _templateRepository.GetTemplateByTemplateType(email.TemplateType);

            if (template != null)
            {
                var emailPayload = _templateLoader.LoadTemplate(template.Payload);
                isSuccesfulSent = await _emailSender.SendEmailAsync(email, emailPayload, cancellationToken);
            }

            var emailStatus = isSuccesfulSent ? EmailStatus.CONFIRMED : EmailStatus.FAILED;
            email.SetEmailStatus(emailStatus);
            await _unitOfWork.EmailWriteRepository().AddEmail(email);

            await _unitOfWork.CompleteAsync();
        }
    }
}
