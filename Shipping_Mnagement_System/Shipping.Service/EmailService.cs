using Microsoft.Extensions.Configuration;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = _config["EmailSettings:SenderEmail"];
            var password = _config["EmailSettings:Password"];

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            var mail = new MailMessage(email, toEmail, subject, body);
            await client.SendMailAsync(mail);
        }
    }

}
