using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Jivar.Service.Implements
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendVerificationEmailAsync(string toEmail, string verificationLink)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
            {
                client.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
                client.EnableSsl = smtpSettings.EnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings.UserName, "Jivar"),
                    Subject = "Verify Your Account",
                    Body = $"<p>Click the link below to verify your account:</p>\r\n<a href=\"{verificationLink}\">Verify Account</a>\r\n",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}