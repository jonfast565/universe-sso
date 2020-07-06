using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using Microsoft.Extensions.Logging;
using UniverseSso.Configuration.Interfaces;
using UniverseSso.Email.Interfaces;

namespace UniverseSso.Email.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private static readonly object ClientLock = new object();
        private readonly SmtpClient _sc;

        public EmailSender(IBackendConfiguration bc, ILogger logger)
        {
            _logger = logger;
            _sc = SetupSmtpClient(bc);
        }

        public void SendEmail(EmailNotification email, CancellationToken ct)
        {
            char[] sep = {';'};

            try
            {
                SanitizeMessageSubject(email);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                var message = new MailMessage
                {
                    Subject = email.EmailSubject,
                    IsBodyHtml = email.IsBodyHtml,
                    From = new MailAddress(email.SentFrom)
                };

                // use this to avoid a plain-text message collapsing when it gets converted to HTML. 
                // The breaks are not preserved so they need to be converted to <br> tags
                var messageText = email.EmailMessage;

                // can view as plain text or html, should fall back based on the client.
                var viewHtml =
                    AlternateView.CreateAlternateViewFromString(
                        messageText, 
                        null, 
                        MediaTypeNames.Text.Html);
                var viewPlain =
                    AlternateView.CreateAlternateViewFromString(
                        email.EmailMessage, 
                        null, 
                        MediaTypeNames.Text.Plain);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                AddAttachmentsToMessage(email, message);

                // no need to specify a message body with alternate views.
                message.AlternateViews.Add(viewHtml);
                message.AlternateViews.Add(viewPlain);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                AddMessageCcs(email, sep, message);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                AddMessageBccs(email, sep, message);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                AddMessageTos(email, sep, message);

                if (ct.IsCancellationRequested)
                {
                    return;
                }

                lock (ClientLock)
                {
                    _sc.Send(message);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Email could not be sent");
            }
        }

        private static SmtpClient SetupSmtpClient(IBackendConfiguration batchConfiguration)
        {
            var sc = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = batchConfiguration.EmailSmtpHost,
                EnableSsl = batchConfiguration.EmailEnableSsl,
                Port = batchConfiguration.EmailSmtpPort,
                UseDefaultCredentials = batchConfiguration.EmailUseDefaultCredentials
            };

            if (batchConfiguration.EmailUsername != null
                && batchConfiguration.EmailPassword != null)
            {
                sc.Credentials = new NetworkCredential(
                    batchConfiguration.EmailUsername,
                    batchConfiguration.EmailPassword);
            }

            return sc;
        }

        private static void SanitizeMessageSubject(EmailNotification email)
        {
            email.EmailSubject = email.EmailSubject
                .Replace("\r", " ")
                .Replace("\n", " ");
        }

        private static void AddAttachmentsToMessage(EmailNotification email, MailMessage message)
        {
            if (!email.EmailAttachments.Any())
            {
                return;
            }

            foreach (var attachment in email.EmailAttachments)
            {
                AddAttachmentToMessage(message, attachment);
            }
        }

        private static void AddMessageTos(EmailNotification email, char[] sep, MailMessage message)
        {
            if (string.IsNullOrEmpty(email.SentTo))
            {
                return;
            }

            var sendTo = email.SentTo.Split(sep,
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in sendTo)
            {
                message.To.Add(t);
            }
        }

        private static void AddMessageBccs(EmailNotification email, char[] sep, MailMessage message)
        {
            if (string.IsNullOrEmpty(email.SentBcc))
            {
                return;
            }

            var sendBcc = email.SentBcc.Split(sep,
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in sendBcc)
            {
                message.Bcc.Add(t);
            }
        }

        private static void AddMessageCcs(EmailNotification email, char[] sep, MailMessage message)
        {
            if (string.IsNullOrEmpty(email.SentCc))
            {
                return;
            }

            var sendCc = email.SentCc.Split(sep,
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in sendCc)
            {
                message.CC.Add(t);
            }
        }

        private static void AddAttachmentToMessage(MailMessage message, EmailAttachment attachment)
        {
            var ms = new MemoryStream(attachment.Contents) {Position = 0};
            message.Attachments.Add(new Attachment(ms, attachment.Filename, attachment.MediaTypeName)
            {
                ContentDisposition = {Inline = false}
            });
        }
    }
}