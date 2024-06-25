using DermDiag.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DermDiag.Repository
{
    public class EmailRepository 
    {
        private readonly string _emailFrom = "ar1750@fayoum.edu.eg";
        private readonly string _emailPassword = "ahmedramadanahmed1448";

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("DermDiag System", _emailFrom));
                message.To.Add(new MailboxAddress("", emailMessage.To));
                message.Subject = emailMessage.Subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = emailMessage.Body };

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(_emailFrom, _emailPassword);

                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex) { return false; }

        }
    }
}
