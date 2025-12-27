using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccommodationBookingPlatform.Application.Email
{
    public class EmailMessageBuilder
    {
        private EmailMessage _msg = new(
            From: string.Empty,
            To: string.Empty,
            Subject: string.Empty,
            HtmlBody: null,
            PlainTextBody: null,
            Attachments: new List<(string, byte[], string)>()
        );

        public EmailMessageBuilder From(string from)
        {
            _msg = _msg with { From = from };
            return this;
        }

        public EmailMessageBuilder To(string to)
        {
            _msg = _msg with { To = to };
            return this;
        }

        public EmailMessageBuilder Subject(string subject)
        {
            _msg = _msg with { Subject = subject };
            return this;
        }

        public EmailMessageBuilder HtmlBody(string html)
        {
            _msg = _msg with { HtmlBody = html };
            return this;
        }

        public EmailMessageBuilder PlainTextBody(string text)
        {
            _msg = _msg with { PlainTextBody = text };
            return this;
        }

        public EmailMessageBuilder AddAttachment(string filename, byte[] content, string contentType)
        {
            var list = new List<(string, byte[], string)>(_msg.Attachments)
            {
                (filename, content, contentType)
            };

            _msg = _msg with { Attachments = list };
            return this;
        }

        public EmailMessage Build()
        {
            if (string.IsNullOrEmpty(_msg.To))
                throw new InvalidOperationException("To is required");

            if (string.IsNullOrEmpty(_msg.Subject))
                throw new InvalidOperationException("Subject is required");

            if (string.IsNullOrEmpty(_msg.HtmlBody) && string.IsNullOrEmpty(_msg.PlainTextBody))
                throw new InvalidOperationException("Either HtmlBody or PlainTextBody must be provided");

            return _msg;
        }
    }
}
