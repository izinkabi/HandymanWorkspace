using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Handyman_DataLibrary.Helpers
{
    public class EmailSender
    {

        public void SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("major81@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("major81@ethereal.email"));
            email.Subject = "Confirm Email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("major81@ethereal.email", "xUxbtMqCzcVf7QsWx9");
            smtp.Send(email);
            smtp.Disconnect(true);

            //return resetPasswordToken;
        }
    }
}
