using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Utilities
{
    public static class Helper
    {
        public static bool SendEmail(string userEmail, string msgArea)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contact.togshop@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Email Təsdiq Mesajı - ToG Shopping";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = msgArea;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("contact.togshop@gmail.com", "ohtiqxjdkojlpqez");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }

    public enum UserRoles
    {
        Admin,
        SuperModerator,
        Moderator,
        User
    }
}
