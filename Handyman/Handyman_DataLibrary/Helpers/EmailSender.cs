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
            email.From.Add(MailboxAddress.Parse("araceli.powlowski@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("araceli.powlowski@ethereal.email"));
            email.Subject = "Confirm Email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("araceli.powlowski@ethereal.email", "6dwzhKNuGPZwgxvmzr");
            smtp.Send(email);
            smtp.Disconnect(true);

            //return resetPasswordToken;
        }
    }
}
