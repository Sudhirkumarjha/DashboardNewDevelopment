using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using VideoAssetManager.CommonUtils.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;


namespace VideoAssetManager.DataAccess.Common
{
    public class EmailSender 
    {
        public string SendGridSecret { get; set; }     
        ILogger<EmailSender> _logger;
     
        public Task SendEmailAsync(EmailMessage message)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.Sender = MailboxAddress.Parse(message.From.Email);

                emailToSend.Sender.Name = message.From.Name;

                emailToSend.From.Add(MailboxAddress.Parse(emailToSend.Sender.ToString()));
                
                emailToSend.To.Add(new MailboxAddress(message.To.Select(x=>x.Name).FirstOrDefault(),message.To.Select(u=>u.Email).FirstOrDefault()));
                if (message.CopyTo!= null)
                    emailToSend.Cc.Add(new MailboxAddress(message.CopyTo.Select(x => x.Name).FirstOrDefault(), message.CopyTo.Select(u => u.Email).FirstOrDefault()));

                if (message.BlindCopyTo!=null)
                    emailToSend.Bcc.Add(new MailboxAddress(message.BlindCopyTo.Select(x => x.Name).FirstOrDefault(), message.BlindCopyTo.Select(u => u.Email).FirstOrDefault()));

                emailToSend.Subject = message.Subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {

                    Text = message.Body
                };

                using (var emailClient = new SmtpClient())
                {
                 
              
                    emailClient.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;

                    emailClient.CheckCertificateRevocation = false;

                    //emailClient.Connect(AppConfig.SmtpConfig.Host, AppConfig.SmtpConfig.Port, true);

                    emailClient.Connect("smtp.gmail.com", 465, true);


                    emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                    //emailClient.Authenticate(AppConfig.SmtpConfig.User, AppConfig.SmtpConfig.Password);
                    emailClient.Authenticate("noreply@rekhta.org", "NOR#PLYP@$$w0rd@REKHTA");

                    emailClient.Send(emailToSend);
                    emailClient.Disconnect(true);
                }

                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                _logger.LogError($"Error while sending email: \"{0}\" ", e);
                return Task.CompletedTask;

            }
        }      
      
  
        }
    }

  
    

