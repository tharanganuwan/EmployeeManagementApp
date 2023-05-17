using DomainLayer.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using ServiceLayer.services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _config["EmailSettings:From"]; 
            emailMessage.From.Add(new MailboxAddress("Employee Web Application", from));
            emailMessage.To.Add(new MailboxAddress(emailModel.To,emailModel.To));
            emailMessage.Subject = emailModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Content)
            };

            using(var clint = new SmtpClient())
            {
                try
                {
                    clint.Connect( _config["EmailSettings:SmtpServer"], 465, true);
                    clint.Authenticate(_config["EmailSettings:UserName"], _config["EmailSettings:password"]);
                    clint.Send(emailMessage);
                }
                catch (Exception )
                {
                    throw;
                }
                finally
                {
                    clint.Disconnect(true);
                    clint.Dispose();
                }
            }
        }
    }
}
