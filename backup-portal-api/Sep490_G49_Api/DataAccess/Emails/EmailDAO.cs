using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DataAccess.Emails
{
    public class EmailDAO
    {
        private readonly IConfiguration _configuration;

        public EmailDAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string recipientEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var smtpUsername = smtpSettings["Username"];
            var smtpPassword = smtpSettings["Password"];
            var smtpHost = smtpSettings["Host"];
            var smtpPort = int.Parse(smtpSettings["Port"]);
            var enableSsl = bool.Parse(smtpSettings["EnableSsl"]);

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = enableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(recipientEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
