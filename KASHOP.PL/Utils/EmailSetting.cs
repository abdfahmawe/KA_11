using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace KASHOP.PL.Utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("abdalrahman.hamdan.gpt@gmail.com", "quky qaog ybuv gwrx")
            };

            return client.SendMailAsync(
                new MailMessage(from: "abdalrahman.hamdan.gpt@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml =true});
            ;
        
         }
    }
}
