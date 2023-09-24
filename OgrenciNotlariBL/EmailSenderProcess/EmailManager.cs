using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OgrenciNotlariBL.EmailSenderProcess
{
    public class EmailManager : IEmailManager
    {
        private readonly IConfiguration _configuration;
        public bool SendEmail(EmailMessageModel model)
        {
            try
            {
                var xx = _configuration.GetSection("SystemEmailOptions:Email").ToString();
                MailMessage message = new MailMessage();
                message.From = new MailAddress(_configuration.GetSection("SystemEmailOptions:Email").Value?.ToString());
                message.To.Add(new MailAddress(model.To));
                message.Subject = model.Subject;
                message.Body = model.Body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(_configuration.GetSection("SystemEmailOptions:Email").Value?.ToString(), _configuration.GetSection("SystemEmailOptions:Token").Value?.ToString());
                client.Port = Convert.ToInt32(_configuration.GetSection("SystemEmailOptions:SmtpPort").Value); ;
                client.Host = _configuration.GetSection("SystemEmailOptions:SmtpHost").Value?.ToString();
                client.EnableSsl = true;

                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
                //loglasın
            }
        }

        public Task SendEmailAsync(EmailMessageModel model)
        {
            throw new NotImplementedException();
        }
    }
}
