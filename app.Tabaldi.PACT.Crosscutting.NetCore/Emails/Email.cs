using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace app.Tabaldi.PACT.Crosscutting.NetCore.Emails
{
    public static class Email
    {
        public static bool Send(string to, string subject, string body)
        {
            try
            {
                var objEmail = new MailMessage
                {
                    From = new MailAddress("fisio.pact@gmail.com", "Fisio Tec system"),
                    Priority = MailPriority.Normal,
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body,
                    SubjectEncoding = Encoding.GetEncoding("ISO-8859-1"),
                    BodyEncoding = Encoding.GetEncoding("ISO-8859-1")
                };
                objEmail.To.Add(to);
                var objSmtp = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("fisio.pact@gmail.com", "testes@123"),
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                objSmtp.Send(objEmail);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
