﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            string password = _configuration["EmailSettings:Password"];
            string SMTP = _configuration["EmailSettings:SMTP"];
            string fromEmail = _configuration["EmailSettings:User"];
            int port = Int32.Parse(_configuration["EmailSettings:PORT"]);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(SMTP, port, MailKit.Security.SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(fromEmail, password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }
}
