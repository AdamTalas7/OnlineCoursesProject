using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineCoursesWeb.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage uzenet = new MailMessage();
            uzenet.From = new MailAddress("onlinecourses.confirmation@gmail.com");
            uzenet.Subject = subject;
            uzenet.To.Add(new MailAddress(email));
            uzenet.Body = "<html><body> " + htmlMessage + "</body></html> ";
            uzenet.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("onlinecourses.confirmation@gmail.com", "prnfpbnafzvxktvo"),
                EnableSsl = true
            };
            smtpClient.Send(uzenet);
            return Task.CompletedTask;
        }
    }
}
