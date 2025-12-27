using AccommodationBookingPlatform.Application.Common.Settings;
using AccommodationBookingPlatform.Application.Contracts.Infrastructure.Services;
using AccommodationBookingPlatform.Application.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AccommodationBookingPlatform.Infrastrucutre.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(EmailMessage msg, CancellationToken ct = default)
        {
            var email = new MimeMessage();

            // From
            email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

            // To
            email.To.Add(MailboxAddress.Parse(msg.To));

            // Subject
            email.Subject = msg.Subject;

            // Body
            var builder = new BodyBuilder();

            if (!string.IsNullOrWhiteSpace(msg.HtmlBody))
                builder.HtmlBody = msg.HtmlBody;

            if (!string.IsNullOrWhiteSpace(msg.PlainTextBody))
                builder.TextBody = msg.PlainTextBody;

            // Attachments
            if (msg.Attachments?.Any() == true)
            {
                foreach (var att in msg.Attachments)
                {
                    var contentType = MimeKit.ContentType.Parse(att.ContentType);
                    builder.Attachments.Add(att.FileName, att.Content, contentType);
                }
            }

            email.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            client.CheckCertificateRevocation = false;

            await client.ConnectAsync(
                _settings.SmtpHost,
                _settings.SmtpPort,
                SecureSocketOptions.StartTls,
                ct);

            await client.AuthenticateAsync(
                _settings.SmtpUser,
                _settings.SmtpPass,
                ct);

            await client.SendAsync(email, ct);
            await client.DisconnectAsync(true, ct);
        }
    }
}
