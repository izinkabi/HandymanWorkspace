namespace Handyman_DataLibrary.Helpers
{
    public class EmailSender
    {

        public void SendEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("alessandra.marquardt@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("alessandra.marquardt@ethereal.email"));
            email.Subject = "Confirm Email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("alex98@ethereal.email", "veWBCbXFZsJZweZ5V1");
            smtp.Send(email);
            smtp.Disconnect(true);

            return resetPasswordToken;
        }
    }
}
