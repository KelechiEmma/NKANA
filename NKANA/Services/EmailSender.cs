using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace NKANA.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<IEmailSender> _logger;
        private readonly EmailConfig _emailConfig = new ();
        
        public EmailSender(ILogger<IEmailSender> logger, IConfiguration config)
        {
            _logger = logger;
            _emailConfig.SmtpServer = config["EmailConfig:SmtpServer"];
            _emailConfig.SmtpPort = config.GetValue<int>("EmailConfig:SmtpPort");
            _emailConfig.SmtpDisplayName = config.GetValue<string>("EmailConfig:EmailSenderDisplayName");
            _emailConfig.NoReplyEmail = config.GetValue<string>("EmailConfig:NoReplyEmail");
            _emailConfig.NoReplyPassword = config.GetValue<string>("EmailConfig:NoReplyPassword");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userDisplayName"></param>
        /// <returns></returns>
        public DeliveryResponse SendEmail(EmailMessage message, string userDisplayName = null)
        {
            var formatted = new EmailMessage(message.To.Select(x => x.Address).ToArray(), message.Subject, message.Content);
            
            var emailMessage = CreateEmailMessage(formatted, new string[] { _emailConfig.NoReplyEmail }, userDisplayName);
            return Send(emailMessage, _emailConfig.NoReplyEmail, _emailConfig.NoReplyPassword) ? new DeliveryResponse { Errors = null, Succeeded = true } : new DeliveryResponse { Errors = null, Succeeded = false };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="from"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="userDisplayName"></param>
        /// <returns></returns>
        public DeliveryResponse SendEmail(EmailMessage message, string[] from, string username = null, string password = null, string userDisplayName = null)
        {
            var emailMessage = CreateEmailMessage(message, from, userDisplayName);
            return Send(emailMessage, username, password) ? new DeliveryResponse { Errors = null, Succeeded = true } : new DeliveryResponse { Errors = null, Succeeded = false };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userDisplayName"></param>
        /// <returns></returns>
        public async Task<DeliveryResponse> SendEmailAsync(EmailMessage message, string userDisplayName = null)
        {
            var formatted = new EmailMessage(message.To.Select(x => x.Address).ToArray(), message.Subject, message.Content);

            var emailMessage = CreateEmailMessage(formatted, new string[] { _emailConfig.NoReplyEmail }, userDisplayName);
            return await SendAsync(emailMessage, _emailConfig.NoReplyEmail, _emailConfig.NoReplyPassword) ? new DeliveryResponse { Errors = null, Succeeded = true } : new DeliveryResponse { Errors = null, Succeeded = false };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="from">The senders of the message</param>
        /// <param name="username">The username of the sender. Usually the mail address - abc@abc.com</param>
        /// <param name="password">The password of the sender's account.</param>
        /// <param name="userDisplayName"></param>
        /// <returns></returns>
        public async Task<DeliveryResponse> SendEmailAsync(EmailMessage message, string[] from, string username = null, string password = null, string userDisplayName = null)
        {
            var emailMessage = CreateEmailMessage(message, from, userDisplayName);
            return await SendAsync(emailMessage, username, password) ? new DeliveryResponse { Errors = null, Succeeded = true } : new DeliveryResponse { Errors = null, Succeeded = false };
        }

        private MimeMessage CreateEmailMessage(EmailMessage message, string[] from, string displayUserName)
        {
            var emailMessage = new MimeMessage();
            string senderDisplayName = displayUserName ?? _emailConfig.SmtpDisplayName ?? "Email Message";
            foreach (string sender in from)
            {
                var ad = MailboxAddress.Parse(sender);
                ad.Name = senderDisplayName;
                emailMessage.From.Add(ad);
            }

            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            BodyBuilder emailBody;
            emailBody = BuildBody(message.Content, message.Attachments);

            emailMessage.Body = emailBody.ToMessageBody();
            return emailMessage;
        }

        private bool Send(MimeMessage mailMessage, string username, string password)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(username, password);

                    client.Send(mailMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Message sending failed from {mailMessage.From} to {mailMessage.To}." +
                        $"{Environment.NewLine}{ex.Message}");
                    return false;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task<bool> SendAsync(MimeMessage mailMessage, string username, string password)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Accept all SSL certificates (in case the server supports STARTTLS)
                    // comment this line in production environment if you do not want to accept any ssl certificates
                    // presented by your mail server, except valid.
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(username, password);

                    await client.SendAsync(mailMessage);
                    _logger.LogInformation($"Email sent from {mailMessage.From} to {mailMessage.To}.");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Email sending failed from {mailMessage.From} to {mailMessage.To}." +
                        $"{Environment.NewLine}{ex.Message}");
                    return false;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private static BodyBuilder BuildBody(string message, IFormFileCollection attachments = null)
        {
            BodyBuilder body = new BodyBuilder
            {
                HtmlBody = message
            };

            if (attachments != null && attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    body.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
            return body;
        }
    }
}
