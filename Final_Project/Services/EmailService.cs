//using System;
//using Final_Project.Services.Interfaces;
//using Final_Project.Helpers;
//using Microsoft.Extensions.Options;
//using System.Net.Mail;
//using MimeKit;
//using MimeKit.Text;
//using MailKit.Security;
//using MailKit.Net.Smtp;

using Final_Project.Helpers;
using Final_Project.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Final_Project.Services
{
    public class EmailService : IEmailService
    {
        //private readonly EmailSettings _emailSettings;

        //public EmailService(IOptions<EmailSettings> emailSettings)
        //{
        //    _emailSettings = emailSettings.Value;
        //}

        //public void Send(string to, string subject, string html, string from = null)
        //{
        //    // create message
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.FromAddress));
        //    email.To.Add(MailboxAddress.Parse(to));
        //    email.Subject = subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = html };

        //    // send email
        //    using var smtp = new MailKit.Net.Smtp.SmtpClient();
        //    smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}


        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();

            // If 'from' parameter is not provided or is empty, use the default 'FromAddress' from the settings
            string fromAddress = !string.IsNullOrEmpty(from) ? from : _emailSettings.FromAddress;

            // Ensure 'fromAddress' is not null or empty before adding to email.From
            if (!string.IsNullOrEmpty(fromAddress))
            {
                email.From.Add(MailboxAddress.Parse(fromAddress));
            }

            // Ensure 'to' address is not null or empty before adding to email.To
            if (!string.IsNullOrEmpty(to))
            {
                email.To.Add(MailboxAddress.Parse(to));
            }

            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
            smtp.Send(email);
        }
    }
}

