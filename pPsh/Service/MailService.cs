using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace pPsh.Service
{
    public class MailService
    {
        public static void Send(string to, string subject, string content)
        {
            var message = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["serviceMailAddress"], to)
            {
                Subject = subject,
                Body = content
            };
            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Credentials = new System.Net.NetworkCredential(
                    ConfigurationManager.AppSettings["smtpUser"],
                    ConfigurationManager.AppSettings["smtpPassword"]),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }

    }
}