﻿using MailKit.Net.Smtp;
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
            email.From.Add(MailboxAddress.Parse("alessandra.marquardt@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("alessandra.marquardt@ethereal.email"));
            email.Subject = "Confirm Email";
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("alessandra.marquardt@ethereal.email", "7szyhRFVTpsrzE2pVs");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
