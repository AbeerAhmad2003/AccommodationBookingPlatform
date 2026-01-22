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
        public async Task SendBookingConfirmationAsync(
          string toEmail,
          string hotelName,
          string roomClassName,
          DateTime checkIn,
          DateTime checkOut,
          int nights,
          int roomsCount,
          IEnumerable<string> allocatedRoomNumbers,
          decimal originalPricePerNight,
          decimal? discountPercentage,
          decimal finalPricePerNight,
          decimal totalPrice,
          string invoiceNumber,
          DateTime invoiceCreatedAtUtc,
          CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                throw new ArgumentException("Recipient email is missing.", nameof(toEmail));

            var roomNumbersText = string.Join(", ", allocatedRoomNumbers ?? Enumerable.Empty<string>());

            var discountText = discountPercentage is null
                ? "No Discount"
                : $"{discountPercentage}%";

            var html = $@"
<div style='font-family:Arial; padding:15px'>
    <h2 style='color:#2c3e50;'>Booking Confirmed 🎉</h2>

    <p>Dear Customer,</p>
    <p>Your booking has been successfully confirmed. Below are your booking details:</p>

    <hr/>

    <h3>🏨 Hotel Information</h3>
    <p>
        <b>Hotel:</b> {hotelName}<br/>
        <b>Room Class:</b> {roomClassName}<br/>
        <b>Check-in:</b> {checkIn:yyyy-MM-dd}<br/>
        <b>Check-out:</b> {checkOut:yyyy-MM-dd}<br/>
        <b>Nights:</b> {nights}<br/>
        <b>Rooms Count:</b> {roomsCount}<br/>
        <b>Allocated Rooms:</b> {roomNumbersText}
    </p>

    <hr/>

    <h3>💰 Pricing Summary</h3>
    <p>
        <b>Original Price / Night:</b> {originalPricePerNight} $<br/>
        <b>Discount Applied:</b> {discountText}<br/>
        <b>Final Price / Night:</b> {finalPricePerNight} $<br/>
        <b>Total:</b> <span style='color:green;font-size:18px;'>{totalPrice} $</span>
    </p>

    <hr/>

    <h3>📄 Invoice</h3>
    <p>
        <b>Invoice Number:</b> {invoiceNumber}<br/>
        <b>Created:</b> {invoiceCreatedAtUtc:yyyy-MM-dd HH:mm} (UTC)
    </p>

    <p>You can download your invoice from your dashboard.</p>

    <br/>

    <p>Thank you for choosing us 💙</p>

    <hr/>
    <p style='font-size:12px;color:#888'>
        Accommodation Booking System
    </p>
</div>";

            var msg = new EmailMessageBuilder()
                .To(toEmail)
                .Subject("Booking Confirmation & Invoice")
                .HtmlBody(html)
                .Build();

            await SendAsync(msg, ct);
        }
    }
}

