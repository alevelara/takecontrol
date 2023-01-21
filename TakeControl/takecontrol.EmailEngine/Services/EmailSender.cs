using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using takecontrol.Application.Contracts.Emails;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Emails.Options;
using takecontrol.Domain.Models.Emails.ValueObjects;
using takecontrol.Domain.Models.Templates;
using Takecontrol.EmailEngine.Helpers;

namespace takecontrol.EmailEngine.Services;

public sealed class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(Email email, string payload, CancellationToken ct = default)
    {
        try
        {
            var mailGenerator = new EmailGenerator();
            mailGenerator.AddSender(_emailSettings.DisplayName, _emailSettings.From, email.EmailTo);
            mailGenerator.AddBody(email.Subject, payload);

            await SendEmailAsync(mailGenerator.Mail, ct);

            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task SendEmailAsync(MimeMessage mail, CancellationToken ct = default)
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
