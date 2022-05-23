using MimeKit;
using MailKit.Net.Smtp;
using System.Text;

namespace EMarket.Service
{
    public class EmailService
    {
        public void SendEmail(string email, string subject, StringBuilder message)
        {
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("iiyama monitors", "emarketpost@yahoo.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;           

            var builder = new BodyBuilder();            
            builder.HtmlBody = string.Format(message.ToString());
            emailMessage.Body = builder.ToMessageBody();
            
            var format = FormatOptions.Default.Clone();
            format.NewLineFormat = NewLineFormat.Dos;
            emailMessage.WriteTo(format, @"EMarketEmail/email.eml");

            using (var client = new SmtpClient())
            {                
                client.Connect("smtp.mail.yahoo.com", 465, true);
                client.Authenticate("emarketpost@yahoo.com", "vtltsqaktxbteqho");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
