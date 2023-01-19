using MailKit.Security;
using MimeKit;
using takecontrol.Domain.Models.Emails.Options;

namespace Takecontrol.EmailEngine.Helpers
{
    public class EmailGenerator
    {
        public MimeMessage Mail { get; private set; }

        public EmailGenerator()
        {
            Mail = new MimeMessage();
        }

        public void AddSender(string displayName, string from, string emailTo)
        {
            Mail.From.Add(new MailboxAddress(displayName, from));
            Mail.Sender = new MailboxAddress(displayName, from);

            Mail.To.Add(MailboxAddress.Parse(emailTo));
        }

        public void AddBody(string subject, string payload)
        {
            var body = new BodyBuilder();
            Mail.Subject = subject;
            body.HtmlBody = payload;
            Mail.Body = body.ToMessageBody();
        }
    }
}
