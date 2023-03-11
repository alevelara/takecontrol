using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Takecontrol.Emails.Application.Contracts.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Emails.Options;
using Takecontrol.Emails.Infrastructure.Helpers;

namespace Takecontrol.Emails.Infrastructure.Repositories.Services;

public sealed class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(Email email, string payload, CancellationToken ct = default)
    {
        try
        {
            var mailGenerator = new EmailGenerator();
            mailGenerator.AddSender(_emailSettings.DisplayName, _emailSettings.From, email.EmailTo);
            mailGenerator.AddBody(email.Subject, payload);

            await SendMimeMessageAsync(mailGenerator.Mail, ct);

            return true;

        }
        catch (Exception e)
        {
            _logger.LogWarning($"Some error occurred during email sending: {e.Message}");
            return false;
        }
    }

    private async Task SendMimeMessageAsync(MimeMessage mail, CancellationToken ct = default)
    {
        using var smtp = new SmtpClient();

        if (_emailSettings.UseSSL)
        {
            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
        }
        else if (_emailSettings.UseStartTls)
        {
            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls, ct);
        }

        await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password, ct);
        await smtp.SendAsync(mail, ct);
        await smtp.DisconnectAsync(true, ct);
    }
}
