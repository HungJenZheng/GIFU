using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GIFU.Tools
{
    public class MailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class EmailSender
    {
        public void SendAnEmail(MailModel mailModel)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(mailModel.To);
            mail.From = new MailAddress(mailModel.From);
            mail.Subject = mailModel.Subject;
            mail.Body = mailModel.Body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Variable.GetSmtpHost;
            smtp.Port = Convert.ToInt32(Variable.GetSmtpPort);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(Variable.GetMailAccount, Variable.GetMailPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public async Task SendAnEmailAsync(MailModel mailModel)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(mailModel.To);
            mail.From = new MailAddress(mailModel.From);
            mail.Subject = mailModel.Subject;
            mail.Body = mailModel.Body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Variable.GetSmtpHost;
            smtp.Port = Convert.ToInt32(Variable.GetSmtpPort);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(Variable.GetMailAccount, Variable.GetMailPassword);
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception)
            {
            }
            return;
        }
    }
}