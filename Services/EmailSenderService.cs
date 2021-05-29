using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly EmailNotificationMetadata _emailNotificationMetadata;
        public EmailSenderService(EmailNotificationMetadata emailNotificationMetadata)
        {
            _emailNotificationMetadata = emailNotificationMetadata;
        }
        public void SendEmail(string email, string subject, string htmlMessage)
        {
            string userName = _emailNotificationMetadata.UserName;
            string password = _emailNotificationMetadata.Password;
            MailMessage msg = new MailMessage(_emailNotificationMetadata.Sender, email);
            msg.Subject = $"{subject}";
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;
            //Attachment attach = new Attachment(Server.MapPath("folder/" + ImgName));
            //msg.Attachments.Add(attach);
            SmtpClient SmtpClient = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(userName, password),
                Host = _emailNotificationMetadata.SmtpServer,
                Port = _emailNotificationMetadata.Port,
                EnableSsl = false
            };
            SmtpClient.Send(msg);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string userName = _emailNotificationMetadata.UserName;
            string password = _emailNotificationMetadata.Password;
            MailMessage msg = new MailMessage(_emailNotificationMetadata.Sender, email);
            msg.Subject = $"{subject}";
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;
            //Attachment attach = new Attachment(Server.MapPath("folder/" + ImgName));
            //msg.Attachments.Add(attach);
            SmtpClient SmtpClient = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(userName, password),
                Host = _emailNotificationMetadata.SmtpServer,
                Port = _emailNotificationMetadata.Port,
                EnableSsl = true
            };
            await SmtpClient.SendMailAsync(msg);
        }

        public void SendEmails(string[] emails, string[] subjects, string[] htmlMessages)
        {
            string userName = _emailNotificationMetadata.UserName;
            string password = _emailNotificationMetadata.Password;
            SmtpClient SmtpClient = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential(userName, password),
                Host = _emailNotificationMetadata.SmtpServer,
                Port = _emailNotificationMetadata.Port,
                EnableSsl = false
            };
            for (int i = 0; i < emails.Length; i++)
            {
                MailMessage msg = new MailMessage(_emailNotificationMetadata.Sender, emails[i]);
                msg.Subject = $"{subjects[i]}";
                msg.Body = htmlMessages[i];
                msg.IsBodyHtml = true;
                //Attachment attach = new Attachment(Server.MapPath("folder/" + ImgName));
                //msg.Attachments.Add(attach);

                SmtpClient.Send(msg);
            }

        }
    }
    public class EmailNotificationMetadata
    {
        public string Sender { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
